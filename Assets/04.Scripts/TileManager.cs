using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject normalTile;
    public GameObject holeTile;
    public GameObject extremeTile;

    public float sizeFator = 2f;
    private float sideInterval = 0.75f;
    private float upperInterval = 0.866f;

    public int tileColumn;
    public int tileRow;

    public int maximumTrapTile;
    public int TrapTileCount { get; set; }

    public int LineCounter { get; set; }
    public int PlayerLineCounter { get; set; }

    //private Vector3 curCenterTilePos = new();
    private List<Vector3> tilePoses = new();
    public List<GameObject> allTiles { get; set; } = new();
    public List<GameObject> destroyTiles { get; set; } = new();

    private void Awake()
    {
        sideInterval *= sizeFator;
        upperInterval *= sizeFator;
        SetTilePoses();
        StartGame();
    }

    private void SetTilePoses()
    {
        tilePoses.Clear();

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
            tilePoses.Add(new Vector3(xPos, 0f, zPos));

            xPos *= -1;
        }
    }

    public void SpawnTile()
    {
        List<GameObject> tiles = new();
        while(LineCounter < PlayerLineCounter + tileColumn)
        {
            foreach (var tilePos in tilePoses)
            {
                var tile = Instantiate(GetRandomTile(), tilePos + new Vector3(0f, 0f, LineCounter * upperInterval * 2), Quaternion.identity);
                tiles.Add(tile);
                tile.transform.localScale *= sizeFator;
                tile.SendMessage("Init", LineCounter);
                allTiles.Add(tile);
            }            
            LineCounter++;
        }
        foreach(var tile in allTiles)
        {
            tile.SendMessage("CheckTile");            
        }
        foreach(var obj in  destroyTiles)
        {
            allTiles.Remove(obj);
            Destroy(obj);
        }
    }
    
    public void StartGame()
    {   
        SpawnTile();

        foreach (GameObject tile in allTiles)
            tile.SendMessage("CheckPlayerRange");
    }

    public void MoveTiles()
    {
        foreach (var tile in allTiles)
        {
            tile.transform.Translate(0f, 0f, -upperInterval * 2);
            tile.SendMessage("MoveTile");
        }
        foreach(var tile in destroyTiles)
        {
            allTiles.Remove(tile);
            Destroy(tile);
        }
    }

    private GameObject GetRandomTile()
    {
        if (maximumTrapTile <= TrapTileCount || LineCounter == 0)
        {
            return normalTile;
        }
        switch (Random.value)
        {
            case < 0.1f:
                TrapTileCount++;
                return holeTile;
            case < 0.2f:
                TrapTileCount++;
                return extremeTile;
            default:
                return normalTile;
        }
    }
 }
