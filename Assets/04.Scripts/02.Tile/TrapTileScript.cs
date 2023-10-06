using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTileScript : TileScript
{
    public override void CheckTile()
    {
        base.CheckTile();

        if(tileManager.PlayerLineCounter - LineNumber > 1)
        {
            tileManager.TrapTileCount--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            GameManager.Instance.GameOver();
        }
    }
}
