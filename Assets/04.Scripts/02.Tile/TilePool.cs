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

    private const int InitOffset = 4;

    [SerializeField] private TileManager tileManager;
    [SerializeField] private StageManager stageManager;

    public List<TileScript> ActiveTile { get; set; } = new();
    [SerializeField] private List<Transform> normalTilePos = new();
    [SerializeField] private List<Transform> holeTilesPos = new();
    [SerializeField] private List<Transform> fallingObjectTilesPos = new();
    [SerializeField] private List<Transform> knockbackTilesPos = new();
    [SerializeField] private List<Transform> holdTilesPos = new();

    private List<List<TileScript>> normalTiles = new();
    private int normalTileCount = 0;
    private List<List<TileScript>> holeTiles = new();
    private int  holeTileCount = 0;
    private List<List<TileScript>> fallingObjectTiles = new();
    private int fallingObjectTileCount = 0;
    private List<List<TileScript>> knockbackTiles = new();
    private int knockbackTileCount = 0;
    private List<List<TileScript>> holdTiles = new();
    private int holdTileCount = 0;

    public void MakePools()
    {
        var stageTileInfos = stageManager.stageTileInfos;
        for (int i = 0; i < stageTileInfos.Length; i++)
        {
            var normalTileTemp = new List<TileScript>();
            normalTiles.Add(normalTileTemp);

            int createTileCount = (tileManager.tileColumn + InitOffset) * tileManager.tileRow + stageTileInfos[i].maximumTrapTile;
            for (int j = 0; j < createTileCount; j++)
            {
                var tileScript = InstanceTile(stageTileInfos[i].normalTile, normalTilePos[i]);
                normalTileTemp.Add(tileScript);
            }

            //타일을 담을 컨테이너 생성
            var holeTileTemp = new List<TileScript>();
            holeTiles.Add(holeTileTemp);
            var fallingObjectTileTemp = new List<TileScript>();
            fallingObjectTiles.Add(fallingObjectTileTemp);
            var knockbackTileTemp = new List<TileScript>();
            knockbackTiles.Add(knockbackTileTemp);
            var holdTileTemp = new List<TileScript>();
            holdTiles.Add(holdTileTemp);

            for (int j = 0; j < stageTileInfos[i].maximumTrapTile; j++)
            {
                var tileScript = InstanceTile(stageTileInfos[i].holeTile, holeTilesPos[i]);
                holeTileTemp.Add(tileScript);
                tileScript = InstanceTile(stageTileInfos[i].fallingObjectTile, fallingObjectTilesPos[i]);
                fallingObjectTileTemp.Add(tileScript);
                tileScript = InstanceTile(stageTileInfos[i].knockbackTile, knockbackTilesPos[i]);
                knockbackTileTemp.Add(tileScript);
                tileScript = InstanceTile(stageTileInfos[i].holdTile, holdTilesPos[i]);
                holdTileTemp.Add(tileScript);
            }
        }
    }

    private TileScript InstanceTile(GameObject tileObject, Transform parent)
    {
        var tile = Instantiate(tileObject, parent);
        tile.transform.localScale *= tileManager.sizeFator;
        tile.SetActive(false);
        var tileScript = tile.GetComponent<TileScript>();
        return tileScript;
    }

    public TileScript SetTile(TileType tileType, int stage)
    {
        TileScript tileScript;
        switch (tileType)
        {
            case TileType.normalTile:
                normalTileCount++;
                if (normalTileCount >= normalTiles[stage - 1].Count)
                {
                    normalTileCount = 0;
                }
                tileScript = normalTiles[stage - 1][normalTileCount];
                break;
            case TileType.holeTile:
                holdTileCount++;
                if (holdTileCount >= holdTiles[stage - 1].Count)
                {
                    holdTileCount = 0;
                }
                tileScript = holeTiles[stage - 1][holdTileCount];
                break;
            case TileType.fallingObjectTile:
                fallingObjectTileCount++;
                if (fallingObjectTileCount >= fallingObjectTiles[stage - 1].Count)
                {
                    fallingObjectTileCount = 0;
                }
                tileScript = fallingObjectTiles[stage - 1][fallingObjectTileCount];
                break;
            case TileType.knockbackTile:
                knockbackTileCount++;
                if (knockbackTileCount >= knockbackTiles[stage - 1].Count)
                {
                    knockbackTileCount = 0;
                }
                tileScript = knockbackTiles[stage - 1][knockbackTileCount];
                break;
            case TileType.holdTile:
                holdTileCount++;
                if (holdTileCount >= holdTiles[stage - 1].Count)
                {
                    holdTileCount = 0;
                }
                tileScript = holdTiles[stage - 1][holdTileCount];
                break;
            default:
                normalTileCount++;
                if (normalTileCount >= normalTiles[stage - 1].Count)
                {
                    normalTileCount = 0;
                }
                tileScript = normalTiles[stage - 1][normalTileCount];
                break;
        }

        tileScript.gameObject.SetActive(true);
        ActiveTile.Add(tileScript);
        return tileScript;
    }

    public void ResetTileCount()
    {
        normalTileCount = 0;
        holeTileCount = 0;
        fallingObjectTileCount = 0;
        knockbackTileCount = 0;
        holdTileCount = 0;
    }

    public void UnsetTile(TileScript tile)
    {
        var pool = tile.tileType switch
        {
            TileType.normalTile => normalTilePos,
            TileType.holeTile => holeTilesPos,
            TileType.fallingObjectTile => fallingObjectTilesPos,
            TileType.knockbackTile => knockbackTilesPos,
            TileType.holdTile => holdTilesPos,
            _ => null
        };
        tile.gameObject.SetActive(false);
        ActiveTile.Remove(tile);
        //tile.transform.SetParent(pool[ts.stage - 1].transform);
    }
}