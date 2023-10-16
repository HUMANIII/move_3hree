using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MithrillPhone : PlayerController
{
    protected override void MovePosition(Vector3 pos)
    {
        var gm = GameManager.Instance;
        if ((gm.State & GameManager.States.IsTrapped) != 0)
            return;

        moveCounter++;
        if (moveCount <= moveCounter)
        {
            pos += prevMoveTo switch
            {
                MoveTo.Forward => new Vector3(0f, 0f, moveUpperInterval * 2),
                MoveTo.Left => new Vector3(-moveSideInterval * 2, 0f, moveUpperInterval),
                MoveTo.Right => new Vector3(moveSideInterval * 2, 0f, moveUpperInterval),
                _ => new Vector3()
            };
            if(TileManager.CheckUnderTile(pos) == null) 
            {
                GameManager.Instance.ReleaseTrap();
                pos += prevMoveTo switch
                {
                    MoveTo.Right => new Vector3(-moveSideInterval * 2, 0f, moveUpperInterval),
                    MoveTo.Left => new Vector3(moveSideInterval * 2, 0f, moveUpperInterval),
                    _ => new Vector3()
                };
            }
            moveCounter = 0;
        }


        if (pos.x == transform.position.x)
        {
            GameManager.Instance.CurScore += scoreFactor;
        }
        else 
        {
            GameManager.Instance.CurScore += scoreFactor / 2;
        }

        pos.y = 2f;
        //var ts = TileManager.CheckUnderTile(pos);
        //if (ts != null)
        //{
        //    ts.GetPos();
        //    if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
        //        pos.y -= 1.4f;
        //}

        

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
        if (tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
        {
            timerScripts.DecreaseMaxTime();
        }
        timerScripts.ResetTimer();
    }
}
