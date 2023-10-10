using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTile : TrapTileScript
{
    public int clickCount;
    private int clickCounter = 0;

    private void Update()
    {
        var gm = GameManager.Instance;
        if((gm.State & GameManager.States.IsTrapped) != 0 && clickCounter >= clickCount)
        {
            gm.ReleaseTrap();            
        }
    }
    
    public void Struggle()
    {
        clickCounter++;
    }
}
