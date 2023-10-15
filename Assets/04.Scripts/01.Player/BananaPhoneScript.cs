using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaPhoneScript : PlayerController
{
    public int BatteryCount = 5;
    protected int BatteryCounter;
    public float restoreTime = 10f;

    public void StackBattery()
    {
        BatteryCounter++;
        if(BatteryCounter >= BatteryCount) 
        {
            tileManager.ActiveOverclock();
            timerScripts.RestoreTime(restoreTime);
        }
    }

    protected override void MovePosition(Vector3 pos)
    {
        var gm = GameManager.Instance;
        if ((gm.State & GameManager.States.IsTrapped) != 0)
            return;

        if (pos.x == transform.position.x)
        {
            GameManager.Instance.CurScore += scoreFactor;
        }
        else
        {
            GameManager.Instance.CurScore += scoreFactor / 2;
        }

        pos.y += 1.8f;
        var ts = TileManager.CheckUnderTile(pos);
        if (ts != null)
        {
            ts.GetPos();
            if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
                pos.y -= 1.4f;
        }

        if ((timerScripts.curMaxTime / 2f) < timerScripts.Timer)
        {
            overclockActiveCounter++;
        }
        else
        {
            overclockActiveCounter = 0;
        }
        MoveObjectAndTriggerEvent(pos);

        if (overclockActiveCounter >= overclockActiveCount)
        {
            tileManager.ActiveOverclock();
            overclockActiveCounter = 0;
        }
        tileManager.SpawnTile();
        tileManager.CheckAllTiles();
    }
}
