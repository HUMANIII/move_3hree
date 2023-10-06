using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject normalTile;
    public GameObject holeTile;
    public GameObject fallingObjectTile;
    public GameObject knockbackTile;

    public GameObject battery;
    public GameObject ram;

    public float sizeFator = 2f;
    private float sideInterval = 0.75f;
    public float upperInterval { get; private set; } = 0.866f;

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
        sideInterval *= sizeFator;
        upperInterval *= sizeFator;
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
                xPos += 2 * sideInterval;
            }
            switch ((i + 1) % 4)
            {
                case 2:
                    zPos += upperInterval;
                    break;
                case 0:
                    zPos -= upperInterval;
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
                var tile = Instantiate(GetRandomTile(out bool isNormalTile), tilePos + new Vector3(0f, 0f, LineCounter * upperInterval * 2), Quaternion.identity);
                tiles.Add(tile);
                tile.transform.localScale *= sizeFator;
                tile.SendMessage("Init", LineCounter);
                if(isNormalTile && !(LineCounter == 0))
                {
                    SpawnItem(tile);
                }
                AllTiles.Add(tile);
            }            
            LineCounter++;
        }
        CheckAllTiles();
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
            tile.SendMessage("CheckTile");
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
            tile.SendMessage("CheckPlayerRange");
    }

    public void MoveTiles()
    {
        foreach (var tile in AllTiles)
        {
            tile.transform.Translate(0f, 0f, -upperInterval * 2);
            tile.SendMessage("MoveTile");
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
        switch (Random.value)
        {
            case < 0.07f:
                TrapTileCount++;
                isNormal = false;
                return holeTile;
            case < 0.14f:
                TrapTileCount++;
                isNormal = false;
                return fallingObjectTile;
            case < 0.2f:
                TrapTileCount++;
                isNormal = false;
                return knockbackTile;
            default:
                isNormal = true;
                return normalTile;
        }
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
