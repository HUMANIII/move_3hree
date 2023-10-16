using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePool : MonoBehaviour
{
    public enum TileType
    {
        normalTile,
        holeTile,
        fallingObjectTile,
        knockbackTile,
        holdTile,
    }

    public TileManager tileManager;
    public StageManager stageManager;

    public GameObject alltiles;
    public List<GameObject> normalTiles = new();
    public List<GameObject> holeTiles = new();
    public List<GameObject> fallingObjectTiles = new();
    public List<GameObject> knockbackTiles = new();
    public List<GameObject> holdTiles = new();

    public void MakePools()
    {
        var stageTileInfos = stageManager.stageTileInfos;
        for (int i = 0; i < stageTileInfos.Length; i++)
        {
            for(int j = 0; j < (tileManager.tileColumn + 4)* tileManager.tileRow + stageTileInfos[i].maximumTrapTile; j++)
            {
                var tile = Instantiate(stageTileInfos[i].normalTile);
                tile.transform.localScale *= tileManager.sizeFator;
                tile.transform.SetParent(normalTiles[i].transform);
                tile.SetActive(false);
            }

            for (int j = 0; j < stageTileInfos[i].maximumTrapTile; j++)
            {
                var tile = Instantiate(stageTileInfos[i].holeTile);
                tile.transform.localScale *= tileManager.sizeFator;
                tile.transform.SetParent(holeTiles[i].transform);
                tile.SetActive(false);
                tile = Instantiate(stageTileInfos[i].fallingObjectTile);
                tile.transform.localScale *= tileManager.sizeFator;
                tile.transform.SetParent(fallingObjectTiles[i].transform);
                tile.SetActive(false);
                tile = Instantiate(stageTileInfos[i].knockbackTile);
                tile.transform.localScale *= tileManager.sizeFator;
                tile.transform.SetParent(knockbackTiles[i].transform);
                tile.SetActive(false);
                tile = Instantiate(stageTileInfos[i].holdTile);
                tile.transform.localScale *= tileManager.sizeFator;
                tile.transform.SetParent(holdTiles[i].transform);
                tile.SetActive(false);
            }
        }
    }

    public GameObject SetTile(TileType tileType, int stage)
    {
        var tile = tileType switch
        {
            TileType.normalTile => normalTiles[stage - 1].transform.GetChild(0),
            TileType.holeTile => holeTiles[stage - 1].transform.GetChild(0),
            TileType.fallingObjectTile => fallingObjectTiles[stage - 1].transform.GetChild(0),
            TileType.knockbackTile => knockbackTiles[stage - 1].transform.GetChild(0),
            TileType.holdTile => holdTiles[stage - 1].transform.GetChild(0),
            _ => null
        };
        tile.gameObject.SetActive(true);
        tile.transform.SetParent(alltiles.transform);
        return tile.gameObject;
    }

    public void UnsetTile(GameObject tile) 
    {
        var ts = tile.GetComponent<TileScript>();
        var pool = ts.tileType switch
        {
            TileType.normalTile => normalTiles,
            TileType.holeTile => holeTiles,
            TileType.fallingObjectTile => fallingObjectTiles,
            TileType.knockbackTile => knockbackTiles,
            TileType.holdTile => holdTiles,
            _ => null
        };
        tile.SetActive(false);
        tile.transform.SetParent(pool[ts.stage - 1].transform);
    }
}
