using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItemScript : ItemScript
{
    public int factor = 50;
    public float minAmount = 0.2f;
    public float decreaseAmount = 0.1f;
    public float defaultAmount = 0.5f;

    protected override void ActiveEffect()
    {
        var amount = defaultAmount - decreaseAmount * GameManager.Instance.CurScore / scoreFactor * factor;
        TimerScripts.RestoreTime(Mathf.Clamp(amount, minAmount, defaultAmount));
    }
}
