using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTileScript : MonoBehaviour
{
    TileManager tileManager;
    private int lineNumber;

    private void Awake()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
    }
    public void Init(int LineNum)
    {
        lineNumber = LineNum;
    }
    public void CheckTile()
    {
        if(tileManager.PlayerLineCounter - lineNumber > 1)
        {
            tileManager.TrapTileCount--;
        }
    }
}
