using System;

public class TrapTileScript : TileScript
{
    public override void CheckTile()
    {
        base.CheckTile();

        if (tileManager.PlayerLineCounter - LineNumber > 1)
        {
            //tileManager.TrapTileCount--;
        }
    }

    private void OnEnable()
    {
        tileManager.TrapTileCount++;
    }

    private void OnDisable()
    {
        tileManager.TrapTileCount--;
    }
}
