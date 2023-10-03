using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject normalTile;
    public GameObject holeTile;
    public GameObject extremeTile;

    public float sizeFator = 2f;
    private float sideInterval = 0.5f;
    private float upperInterval = 0.433f;

    public int tileColumn;
    public int tileRow;

    private GameObject currnetCenterTile;

    private void Awake()
    {
        if (tileColumn == 0)
            tileColumn = 7;
        if (tileRow == 0)
            tileRow = 5;
        sideInterval *= sizeFator;
        upperInterval *= sizeFator;
    }
    public void SpawnTile()
    {
        Vector3 pos = currnetCenterTile.transform.position;
        pos.y += upperInterval * 2;
        currnetCenterTile = Instantiate(GetRandomTile(), pos, Quaternion.identity);

    }
    
    public void StartGame()
    {
        currnetCenterTile = Instantiate(normalTile, new Vector3(), Quaternion.identity);

    }

    private GameObject GetRandomTile()
    {
        return default;
    }
 }
