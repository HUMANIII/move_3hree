using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button UpgradeMaxTimeBtn; 
    public int UpgradeMaxTimePrice;
    public Button UpgradeBatteryItemBtn; 
    public int UpgradeBatteryItemPrice;
    public Button UpgradeRamItemBtn; 
    public int UpgradeRamItemPrice;
    public Button UpgradeKnockbackResistBtn; 
    public int UpgradeKnockbackResistPrice;
    public Button UpgradeOverclockEfficiencyBtn; 
    public int UpgradeOverclockEfficiencyPrice;
    public Button UpgradeOverclockOptimizationBtn; 
    public int UpgradeOverclockOptimizationPrice;
    public Button UnlockJailbreakedPhoneBtn; 
    public int UnlockJailbreakedPhonePrice;
    public Button UnlockGreenApplePhoneBtn; 
    public int UnlockGreenApplePhonePrice;
    public Button UnlockBananaPhoneBtn; 
    public int UnlockBananaPhonePrice;
    public Button UnlockMithrillPhoneBtn; 
    public int UnlockMithrillPhonePrice;
    public Button Btn; 
    public int Price;

    private void Awake()
    {
        UpgradeMaxTimeBtn.onClick.AddListener(UpgradeMaxTime);
        UpgradeBatteryItemBtn.onClick.AddListener(UpgradeBatteryItem);
        UpgradeRamItemBtn.onClick.AddListener(UpgradeRamItem);
        UpgradeKnockbackResistBtn.onClick.AddListener(UpgradeKnockbackResist);
        UpgradeOverclockEfficiencyBtn.onClick.AddListener(UpgradeOverclockEfficiency);
        UpgradeOverclockOptimizationBtn.onClick.AddListener(UpgradeOverclockOptimization);
        UnlockJailbreakedPhoneBtn.onClick.AddListener(UnlockJailbreakedPhone);
        UnlockGreenApplePhoneBtn.onClick.AddListener(UnlockGreenApplePhone);
        UnlockBananaPhoneBtn.onClick.AddListener(UnlockBananaPhone);
        UnlockMithrillPhoneBtn.onClick.AddListener(UnlockMithrillPhone);
        var psm = PlayerStatManager.Instance;
        CalcValue(ref UpgradeMaxTimePrice, psm.upgrade.maxTime + 1);
        CalcValue(ref UpgradeBatteryItemPrice, psm.upgrade.batteryItem + 1);
        CalcValue(ref UpgradeRamItemPrice, psm.upgrade.ramItem + 1);
        CalcValue(ref UpgradeOverclockEfficiencyPrice, psm.upgrade.overclockEfficiency + 1);
        CalcValue(ref UpgradeOverclockOptimizationPrice, psm.upgrade.overclockOptimization + 1);        
    }
    private void CalcValue(ref int value, int raise )
    {
        value = (int)Mathf.Pow( value, raise );
    }

    public void UpgradeMaxTime()
    {
        var gm = GameManager.Instance;
        if (UpgradeMaxTimePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeMaxTime()))
            {
                gm.RamCount -= UpgradeMaxTimePrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UpgradeBatteryItem()
    {
        var gm = GameManager.Instance;
        if (UpgradeBatteryItemPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeBatteryItem()))
            {
                gm.RamCount -= UpgradeBatteryItemPrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UpgradeRamItem()
    {
        var gm = GameManager.Instance;
        if (UpgradeRamItemPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeRamItem()))
            {
                gm.RamCount -= UpgradeRamItemPrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UpgradeKnockbackResist()
    {
        var gm = GameManager.Instance;
        if (UpgradeKnockbackResistPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeKnockbackResist()))
            {
                gm.RamCount -= UpgradeKnockbackResistPrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UpgradeOverclockEfficiency()
    {
        var gm = GameManager.Instance;
        if (UpgradeOverclockEfficiencyPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeOverclockEfficiency()))
            {
                gm.RamCount -= UpgradeOverclockEfficiencyPrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UpgradeOverclockOptimization()
    {
        var gm = GameManager.Instance;
        if (UpgradeOverclockOptimizationPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeOverclockOptimization()))
            {
                gm.RamCount -= UpgradeOverclockOptimizationPrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UnlockJailbreakedPhone()
    {
        var gm = GameManager.Instance;
        if (UnlockJailbreakedPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockJailbreakedPhone()))
            {
                gm.RamCount -= UnlockJailbreakedPhonePrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UnlockGreenApplePhone()
    {
        var gm = GameManager.Instance;
        if (UnlockGreenApplePhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockGreenApplePhone()))
            {
                gm.RamCount -= UnlockGreenApplePhonePrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UnlockBananaPhone()
    {
        var gm = GameManager.Instance;
        if (UnlockBananaPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockBananaPhone()))
            {
                gm.RamCount -= UnlockBananaPhonePrice;
            }
            return;
        }
        Anouncement(false);
    }
    public void UnlockMithrillPhone()
    {
        var gm = GameManager.Instance;
        if (UnlockMithrillPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockMithrillPhone()))
            {
                gm.RamCount -= UnlockMithrillPhonePrice;
            }
            return;
        }
        Anouncement(false);
    }

    public bool Anouncement(bool success)
    {
        return success;
    }
}
