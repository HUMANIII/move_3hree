using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject normalTile;
    public GameObject holeTile;
    public float holeTileSpawnRate;
    public GameObject fallingObjectTile;
    public float fallingObjectTileSpawnRate;
    public GameObject knockbackTile;
    public float knockbackTileSpawnRate;
    public GameObject holdTile;
    public float holdTileSpawnRate;

    public GameObject battery;
    public GameObject ram;

    public readonly float sizeFator = 2f;
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
    public List<GameObject> AllTiles { get; set; } = new();
    public List<GameObject> DestroyTiles { get; set; } = new();

    private void Awake()
    {
        SideInterval *= sizeFator;
        UpperInterval *= sizeFator;
        SetTilePoses();
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

        foreach (GameObject tile in AllTiles)
            tile.SendMessage("CheckPlayerRange", SendMessageOptions.DontRequireReceiver);
    }

    public void MoveTiles()
    {
        foreach (var tile in AllTiles)
        {
            tile.transform.Translate(0f, 0f, -UpperInterval * 2);
            tile.SendMessage("MoveTile", SendMessageOptions.DontRequireReceiver);
        }
        foreach(var tile in DestroyTiles)
        {
            AllTiles.Remove(tile);
            Destroy(tile);
        }
    }

    private GameObject GetRandomTile(out bool isNormal)
    {
        if (maximumTrapTile <= TrapTileCount || LineCounter == 0)
        {
            isNormal = true;
            return normalTile;
        }

        var rv = Random.value;
        var rate = holdTileSpawnRate;
        if (rv < rate)
        {
            TrapTileCount++;
            isNormal = false;
            return holdTile;
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
 }
