using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjTile : TrapTileScript
{
    public GameObject warningSign;
    public GameObject fallingObj;

    public int anounceTime;

    private bool isSignSetted = false;

    public void SetWarnSign()
    {
        var trans = warningSign.transform;
        trans.LookAt(player.transform);
        var rot = trans.rotation.eulerAngles;
        rot.x = 0;
        trans.rotation = Quaternion.Euler(rot);
        warningSign.SetActive(true);

        isSignSetted = true;
    }
    public void FallObj()
    {
        fallingObj.SetActive(true);
    }

    public override void CheckPlayerRange()
    {
        int playerDistance = LineNumber - tileManager.PlayerLineCounter;
        if (playerDistance  <= anounceTime && !isSignSetted)
        {
            SetWarnSign();
        }
        if (player.transform.position.z >= transform.position.z)
        {
            FallObj();
        }
        base.CheckPlayerRange();
    }
}
