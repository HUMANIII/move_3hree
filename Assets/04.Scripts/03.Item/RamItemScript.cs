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
    public bool overclocked = false;
    private float ramGatheringFactor;

    protected override void Awake()
    {
        base.Awake();
        var ps = PlayerStatManager.Instance;
        var amount = ps.ramItemRate * ps.upgrade.ramItem;
        minAmount += amount;
        maxAmount +=  amount; 
        defaultAmount += amount;
        if(tileManager.Overclocked)
        {
            overclocked = true;
        }
        ramGatheringFactor = player.GetComponentInParent<PlayerController>().ramGatheringFactor;
    }

    protected override void ActiveEffect()
    {        
        var gm = GameManager.Instance;
        var counter = (factor / (gm.CurScore / scoreFactor)) + defaultAmount;
        gm.RamCount += (int)(Mathf.Clamp(counter, minAmount, maxAmount) * ramGatheringFactor);
    }

    protected override void OnDestroy()
    {
        if(!overclocked)
        {
            base.OnDestroy();
        }
    }
}
