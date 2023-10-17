using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public UIMatching ui;

    public Button UpgradeMaxTimeBtn; 
    public int UpgradeMaxTimePrice;
    public MaskedMaterial maxTimeBar;

    public Button UpgradeBatteryItemBtn; 
    public int UpgradeBatteryItemPrice;
    public MaskedMaterial batteryBar;

    public Button UpgradeRamItemBtn; 
    public int UpgradeRamItemPrice;
    public MaskedMaterial ramBar;

    public Button UpgradeKnockbackResistBtn; 
    public int UpgradeKnockbackResistPrice;
    public MaskedMaterial knockbackBar;

    public Button UpgradeOverclockEfficiencyBtn; 
    public int UpgradeOverclockEfficiencyPrice;
    public MaskedMaterial overclockEfficiencyBar;

    public Button UpgradeOverclockOptimizationBtn; 
    public int UpgradeOverclockOptimizationPrice;
    public MaskedMaterial overclockOptimizationBar;

    public Button UnlockJailbreakedPhoneBtn; 
    public int UnlockJailbreakedPhonePrice;
    public MaskedMaterial jailbreakBar;

    public Button UnlockGreenApplePhoneBtn; 
    public int UnlockGreenApplePhonePrice;
    public MaskedMaterial GreenAppleBar;

    public Button UnlockBananaPhoneBtn; 
    public int UnlockBananaPhonePrice;
    public MaskedMaterial bananaBar;

    public Button UnlockMithrillPhoneBtn; 
    public int UnlockMithrillPhonePrice;
    public MaskedMaterial mithrillBar;

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
    }

    private void Start()
    {
        UpdateState();
    }
    private void CalcValue(ref int value, int raise )
    {
        value *= (int)Mathf.Pow( 2, raise );
    }

    public void UpgradeMaxTime()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeMaxTimePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeMaxTime()))
            {
                gm.RamCount -= UpgradeMaxTimePrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UpgradeBatteryItem()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeBatteryItemPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeBatteryItem()))
            {
                gm.RamCount -= UpgradeBatteryItemPrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UpgradeRamItem()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeRamItemPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeRamItem()))
            {
                gm.RamCount -= UpgradeRamItemPrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UpgradeKnockbackResist()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeKnockbackResistPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeKnockbackResist()))
            {
                gm.RamCount -= UpgradeKnockbackResistPrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UpgradeOverclockEfficiency()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeOverclockEfficiencyPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeOverclockEfficiency()))
            {
                gm.RamCount -= UpgradeOverclockEfficiencyPrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UpgradeOverclockOptimization()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UpgradeOverclockOptimizationPrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UpgradeOverclockOptimization()))
            {
                gm.RamCount -= UpgradeOverclockOptimizationPrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UnlockJailbreakedPhone()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UnlockJailbreakedPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockJailbreakedPhone()))
            {
                gm.RamCount -= UnlockJailbreakedPhonePrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UnlockGreenApplePhone()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UnlockGreenApplePhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockGreenApplePhone()))
            {
                gm.RamCount -= UnlockGreenApplePhonePrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UnlockBananaPhone()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UnlockBananaPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockBananaPhone()))
            {
                gm.RamCount -= UnlockBananaPhonePrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }
    public void UnlockMithrillPhone()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if (UnlockMithrillPhonePrice <= gm.RamCount)
        {
            if (Anouncement(PlayerStatManager.Instance.UnlockMithrillPhone()))
            {
                gm.RamCount -= UnlockMithrillPhonePrice;
                UpdateState();
            }
            ui.UpdateRamCounter();
            return;
        }
        Anouncement(false);
    }

    public bool Anouncement(bool success)
    {
        return success;
    }

    public void UpdateState()
    {
        var psm = PlayerStatManager.Instance;
        CalcValue(ref UpgradeMaxTimePrice, psm.upgrade.maxTime);
        CalcValue(ref UpgradeBatteryItemPrice, psm.upgrade.batteryItem);
        CalcValue(ref UpgradeRamItemPrice, psm.upgrade.ramItem);
        CalcValue(ref UpgradeOverclockEfficiencyPrice, psm.upgrade.overclockEfficiency);
        CalcValue(ref UpgradeOverclockOptimizationPrice, psm.upgrade.overclockOptimization);

        maxTimeBar.RemovedSegments = maxTimeBar.SegmentCount - psm.upgrade.maxTime;
        batteryBar.RemovedSegments = batteryBar.SegmentCount - psm.upgrade.batteryItem;
        ramBar.RemovedSegments = ramBar.SegmentCount - psm.upgrade.ramItem;
        overclockEfficiencyBar.RemovedSegments = overclockEfficiencyBar.SegmentCount - psm.upgrade.overclockEfficiency;
        overclockOptimizationBar.RemovedSegments = overclockOptimizationBar.SegmentCount - psm.upgrade.overclockOptimization;

        SetBoolSegment(psm.upgrade.knockbackResist == 0, knockbackBar);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.JailbreakedPhone) != 0, jailbreakBar);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.GreenApplePhone) != 0, GreenAppleBar);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.BananaPhone) != 0, bananaBar);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.MithrillPhone) != 0, mithrillBar);
    }

    private void SetBoolSegment(bool info, MaskedMaterial mtr)
    {
        if(info)
        {
            mtr.RemovedSegments = 0;
        }
        else 
        {
            mtr.RemovedSegments = 1;
        }
    }
}
