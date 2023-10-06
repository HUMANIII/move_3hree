using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RamItemScript : ItemScript
{
    public int factor = 50;
    public int maxAmount = 5;
    public int minAmount = 2;
    public int defaultAmount = 2;
    protected override void ActiveEffect()
    {
        var gm = GameManager.Instance;
        var counter = (factor / (gm.CurScore / scoreFactor)) + defaultAmount;
        gm.RamCount += Mathf.Clamp(counter, minAmount, maxAmount);
        Debug.Log(gm.RamCount);
    }
}
