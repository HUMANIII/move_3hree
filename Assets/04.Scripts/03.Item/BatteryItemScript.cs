using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItemScript : ItemScript
{
    public int factor = 50;
    public float minAmount = 0.2f;
    public float decreaseAmount = 0.1f;
    public float defaultAmount = 0.5f;

    private TimerScripts _ts;

    private BananaPhoneScript _bananaPhoneScript;

    private void Start()
    {
        var ps = PlayerStatManager.Instance;
        defaultAmount += ps.upgrade.batteryItem * ps.batteryItemRate;
        _ts ??= GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
        _bananaPhoneScript ??= player.GetComponentInParent<BananaPhoneScript>();
    }
    
    //TODO : 배터리 아이템 시간이 줄어든다는 시각적 이펙트가 필요함
    protected override void ActiveEffect()
    {
        if(PlayerStatManager.Instance.playerType != PlayerStatManager.PlayerType.BananaPhone) 
        {
            var amount = defaultAmount - decreaseAmount * GameManager.Instance.CurScore / scoreFactor * factor;
            _ts.RestoreTime(Mathf.Clamp(amount, minAmount, defaultAmount));
        }
        else
        {
            _bananaPhoneScript.StackBattery();
        }
        EffectPool.BatteryEffect(transform.position);
    }
}
