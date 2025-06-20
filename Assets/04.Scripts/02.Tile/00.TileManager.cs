using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Pool;

public class TileManager : MonoBehaviour
{
    public StageManager StageManager;

    private int curStageNumber = 1;
    public int nextStateScore;

    public TilePool tilePool;

    private float holeTileSpawnRate;
    private float fallingObjectTileSpawnRate;
    private float knockbackTileSpawnRate;
    private float holdTileSpawnRate;

    public GameObject battery;
    public GameObject ram;

    public float sizeFator = 2f;
    public float SideInterval { get; private set; } = 0.75f;
    public float UpperInterval { get; private set; } = 0.866f;

    public int tileColumn;
    public int tileRow;

    public int maximunItemNumber = 2;
    public int maximumTrapTile;
    public int TrapTileCount { get; set; }
    public int ItemCount { get; set; } = 0;

    public int LineCounter { get; set; }
    public int PlayerLineCounter { get; set; }

    private readonly List<Vector3> TilePoses = new();

    public int overclockCount = 5;
    private int leftOverclockCount = 0;
    public bool Overclocked { private set; get; } = false;

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
        tileColumn += GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().warnEarly;
        tilePool.MakePools();
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
            //Debug.Log(curStageNumber);
            tilePool.ResetTileCount();
        }
        List<GameObject> tiles = new();
        while(LineCounter < PlayerLineCounter + tileColumn)
        {
            foreach (var tilePos in TilePoses)
            {
                var tile = GetRandomTile(out bool isNormalTile);
                tile.transform.position = new Vector3(0f, 0f, LineCounter * UpperInterval * 2) + tilePos;
                //tiles.Add(tile);
                tile.Init(LineCounter);
                if(isNormalTile && LineCounter != 0)
                {
                    SpawnItem(tile.gameObject);
                }
                tilePool.ActiveTile.Add(tile);
            }
            LineCounter++;
            if(Overclocked) 
            {
                leftOverclockCount--;
            }
            if(leftOverclockCount <= 1)
            {
                Overclocked = false;
            }
        }
        //foreach(var obj in  DestroyTiles)
        //{
        //    AllTiles.Remove(obj);
        //    Destroy(obj);
        //}
    }

    public void CheckAllTiles()
    {
        foreach (var tile in tilePool.ActiveTile)
        {
            tile.CheckTile();
        }
    }

    private void SpawnItem(GameObject tile)
    {
        var item = GetRandomItem();
        if (item)
        {
            Instantiate(item, tile.transform);
        }
    }

    private void StartGame()
    {   
        SpawnTile();
        SetStage(curStageNumber);
        foreach (var tile in tilePool.ActiveTile)
            tile.CheckPlayerRange();
    }

    private TileScript GetRandomTile(out bool isNormal)
    {
        if (Overclocked || maximumTrapTile <= TrapTileCount || LineCounter == 0)
        {
            isNormal = true;
            return tilePool.SetTile(TilePool.TileType.normalTile, curStageNumber);
        }

        var rv = Random.value;
        var rate = holeTileSpawnRate;
        if (rv < rate)
        {
            //TrapTileCount++;
            isNormal = false;
            return tilePool.SetTile(TilePool.TileType.holeTile, curStageNumber);
        }
        rate += fallingObjectTileSpawnRate;
        if (rv < rate)
        {
            //TrapTileCount++;
            isNormal = false;
            return tilePool.SetTile(TilePool.TileType.fallingObjectTile, curStageNumber);
        }
        rate += knockbackTileSpawnRate;
        if (rv < rate)
        {
            //TrapTileCount++;
            isNormal = false;
            return tilePool.SetTile(TilePool.TileType.knockbackTile, curStageNumber);
        }
        rate += holdTileSpawnRate;
        if (rv < rate)
        {
            //TrapTileCount++;
            isNormal = false;
            return tilePool.SetTile(TilePool.TileType.holdTile, curStageNumber);
        }
        isNormal = true;
        return tilePool.SetTile(TilePool.TileType.normalTile, curStageNumber);
    }
    
    private GameObject GetRandomItem()
    {
        if (Overclocked)
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
        Physics.Raycast(pos, Vector3.down, out var hitInfo, maxDistance: 10f);
        if (hitInfo.collider != null) 
            return hitInfo.collider.GetComponent<TileScript>();
        
        GameManager.Instance.IsTrapped();
        return null;
    }

    private void SetStage(int stage)
    {
        var si = StageManager.GetStageTileInfo(stage);
        holeTileSpawnRate = si.holeTileSpawnRate;
        fallingObjectTileSpawnRate = si.fallingObjectTileSpawnRate;
        knockbackTileSpawnRate = si.knockbackTileSpawnRate;
        holdTileSpawnRate = si.holdTileSpawnRate;
        maximumTrapTile = si.maximumTrapTile;
        nextStateScore = si.nextStateScore;
        RenderSettings.skybox = si.skyBoxMaterial;
        DynamicGI.UpdateEnvironment();
        SoundManager.Instance.PlayStageBGM(stage);
    }

    public void ActiveOverclock()
    {
        leftOverclockCount = overclockCount;
        Overclocked = true;
    }
 }
