using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public StageManager StageManager;

    private int curStageNumber = 1;
    public int nextStateScore;

    private GameObject normalTile;
    private GameObject holeTile;
    private float holeTileSpawnRate;
    private GameObject fallingObjectTile;
    private float fallingObjectTileSpawnRate;
    private GameObject knockbackTile;
    private float knockbackTileSpawnRate;
    private GameObject holdTile;
    private float holdTileSpawnRate;

    public GameObject battery;
    public GameObject ram;

    public float sizeFator = 2f;
    public float SideInterval { get; private set; } = 0.75f;
    public float UpperInterval { get; private set; } = 0.866f;

    public int tileColumn;
    public int tileRow;

    public int maximunItemNumber = 2;
    private int maximumTrapTile;
    public int TrapTileCount { get; set; }
    public int ItemCount { get; set; } = 0;

    public int LineCounter { get; set; }
    public int PlayerLineCounter { get; set; }

    private readonly List<Vector3> TilePoses = new();
    public List<GameObject> AllTiles { get; set; } = new();
    public List<GameObject> DestroyTiles { get; set; } = new();

    public int overclockCount = 5;
    private int leftOverclockCount = 0;

    private void Awake()
    {
        SideInterval *= sizeFator;
        UpperInterval *= sizeFator;
        SetTilePoses();
        SetStage(curStageNumber);
        var psm = PlayerStatManager.Instance;
        overclockCount += psm.upgrade.overclockEfficiency * psm.overclockEfficiencyRate;
    }

    private void Start()
    {
        StartGame();
    }

    private void SetTilePoses()
    {
        TilePoses.Clear();

        float xPos = 0f;
        float zPos = 0f;

        for(int i  = 0; i < tileRow; i++)
        {
            if((i + 1) % 2 == 0)
            {
                xPos += 2 * SideInterval;
            }
            switch ((i + 1) % 4)
            {
                case 2:
                    zPos += UpperInterval;
                    break;
                case 0:
                    zPos -= UpperInterval;
                    break;
            }
            TilePoses.Add(new Vector3(xPos, 0f, zPos));

            xPos *= -1;
        }
    }

    public void SpawnTile()
    {
        if(nextStateScore != 0 && nextStateScore < GameManager.Instance.CurScore)
        {
            curStageNumber++;
            SetStage(curStageNumber);
        }
        List<GameObject> tiles = new();
        while(LineCounter < PlayerLineCounter + tileColumn)
        {
            foreach (var tilePos in TilePoses)
            {
                var tile = Instantiate(GetRandomTile(out bool isNormalTile), tilePos + new Vector3(0f, 0f, LineCounter * UpperInterval * 2), Quaternion.identity);
                tiles.Add(tile);
                tile.transform.localScale *= sizeFator;
                tile.SendMessage("Init", LineCounter,SendMessageOptions.DontRequireReceiver);
                if(isNormalTile && !(LineCounter == 0))
                {
                    SpawnItem(tile);
                }
                AllTiles.Add(tile);
            }            
            LineCounter++;
            if(leftOverclockCount > 0) 
            {
                leftOverclockCount--;
            }
        }
        foreach(var obj in  DestroyTiles)
        {
            AllTiles.Remove(obj);
            Destroy(obj);
        }
    }

    public void CheckAllTiles()
    {
        foreach (var tile in AllTiles)
        {
            tile.SendMessage("CheckTile", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SpawnItem(GameObject tile)
    {
        var item = GetRandomItem();
        if (item != null)
        {
            Instantiate(item, tile.transform);
        }
    }
    
    public void StartGame()
    {   
        SpawnTile();
        //SetStage(stageNumber);
        foreach (GameObject tile in AllTiles)
            tile.SendMessage("CheckPlayerRange", SendMessageOptions.DontRequireReceiver);
    }

    private GameObject GetRandomTile(out bool isNormal)
    {
        if (leftOverclockCount > 0 || maximumTrapTile <= TrapTileCount || LineCounter == 0)
        {
            isNormal = true;
            return normalTile;
        }

        var rv = Random.value;
        var rate = holeTileSpawnRate;
        if (rv < rate)
        {
            TrapTileCount++;
            isNormal = false;
            return holeTile;
        }
        rate += fallingObjectTileSpawnRate;
        if (rv < rate)
        {
            TrapTileCount++;
            isNormal = false;
            return fallingObjectTile;
        }
        rate += knockbackTileSpawnRate;
        if (rv < rate)
        {
            TrapTileCount++;
            isNormal = false;
            return knockbackTile;
        }
        rate += holdTileSpawnRate;
        if (rv < rate)
        {
            TrapTileCount++;
            isNormal = false;
            return holdTile;
        }
        isNormal = true;
        return normalTile;
    }
    
    private GameObject GetRandomItem()
    {
        if (leftOverclockCount > 0)
        {
            return ram;
        }
        if (maximunItemNumber <= ItemCount)
        {
            return null;
        }
        switch (Random.value)
        {
            case < 0.1f:
                ItemCount++;
                return ram;
            case < 0.2f:
                ItemCount++;
                return battery;
            default:
                return null;
        }
    }
    public static TileScript CheckUnderTile(Vector3 pos)
    {
        Physics.Raycast(pos, Vector3.down, out var hitInfo, 10f);
        if (hitInfo.collider == null)
        {
            GameManager.Instance.IsTrapped();
            return null;
        }
        return hitInfo.collider.GetComponent<TileScript>();
    }

    private void SetStage(int stage)
    {
        var si = StageManager.GetStageTileInfo(stage);
        normalTile = si.normalTile;
        holeTile = si.holeTile;
        holeTileSpawnRate = si.holeTileSpawnRate;
        fallingObjectTile = si.fallingObjectTile;
        fallingObjectTileSpawnRate = si.fallingObjectTileSpawnRate;
        knockbackTile = si.knockbackTile;
        knockbackTileSpawnRate = si.knockbackTileSpawnRate;
        holdTile = si.holdTile;
        holdTileSpawnRate = si.holdTileSpawnRate;
        maximumTrapTile = si.maximumTrapTile;
        nextStateScore = si.nextStateScore;
    }

    public void ActiveOverclock()
    {
        leftOverclockCount = overclockCount;
    }
 }
