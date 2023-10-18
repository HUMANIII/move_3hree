using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTileScript : TileScript
{
    public override void CheckTile()
    {
        base.CheckTile();

        if (tileManager.PlayerLineCounter - LineNumber > 1)
        {
            tileManager.TrapTileCount--;
        }
    }

}
