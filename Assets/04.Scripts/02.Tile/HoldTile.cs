using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTile : TrapTileScript
{
    public int clickCount;
    private int clickCounter = 0;

    private void Start()
    {
        clickCount *= player.GetComponent<PlayerController>().obstructionFactor;
    }


    public void Struggle()
    {
        Debug.Log("Struggled");
        clickCounter++;

        if(clickCounter >= clickCount)
        {
            player.GetComponent<PlayerController>().ReleaseHold();
            Debug.Log("Released");
        }        
    }
}
