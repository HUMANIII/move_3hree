using RengeGames.HealthBars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.QuantumUI;

public class ShopButton : MonoBehaviour
{
    //public UIMatching ui;

    public Button UpgradeMaxTimeBtn; 
    public int UpgradeMaxTimePrice;
    public QUI_Bar maxTimeBar;

    public Button UpgradeBatteryItemBtn; 
    public int UpgradeBatteryItemPrice;
    public QUI_Bar batteryBar;

    public Button UpgradeRamItemBtn; 
    public int UpgradeRamItemPrice;
    public QUI_Bar ramBar;

    public Button UpgradeKnockbackResistBtn; 
    public int UpgradeKnockbackResistPrice;
    public QUI_Bar knockbackBar;

    public Button UpgradeOverclockEfficiencyBtn; 
    public int UpgradeOverclockEfficiencyPrice;
    public QUI_Bar overclockEfficiencyBar;

    public Button UpgradeOverclockOptimizationBtn; 
    public int UpgradeOverclockOptimizationPrice;
    public QUI_Bar overclockOptimizationBar;

    public Button UnlockJailbreakedPhoneBtn; 
    public int UnlockJailbreakedPhonePrice;
    public GameObject jailbreak;

    public Button UnlockGreenApplePhoneBtn; 
    public int UnlockGreenApplePhonePrice;
    public GameObject GreenApple;

    public Button UnlockBananaPhoneBtn; 
    public int UnlockBananaPhonePrice;
    public GameObject banana;

    public Button UnlockMithrillPhoneBtn; 
    public int UnlockMithrillPhonePrice;
    public GameObject mithrill;

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

        maxTimeBar.fillAmount = psm.upgrade.maxTime - psm.maxTimeLimit / (float)psm.maxTimeLimit;
        batteryBar.fillAmount = psm.upgrade.batteryItem - psm.batteryItemLimit / (float)psm.batteryItemLimit;
        ramBar.fillAmount = psm.upgrade.ramItem - psm.ramItemLimit / (float)psm.ramItemLimit;
        overclockEfficiencyBar.fillAmount = psm.upgrade.overclockEfficiency - psm.overclockEfficiencyLimit / (float)psm.overclockEfficiencyLimit;
        overclockOptimizationBar.fillAmount = psm.upgrade.overclockOptimization - psm.overclockOptimizationLimit / (float)psm.overclockOptimizationLimit;

        SetBoolSegment(psm.upgrade.knockbackResist == 0, knockbackBar);

        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.JailbreakedPhone) != 0, jailbreak);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.GreenApplePhone) != 0, GreenApple);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.BananaPhone) != 0, banana);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.MithrillPhone) != 0, mithrill);

        //ui.UpdateRamCounter();
    }

    private void SetBoolSegment(bool info, QUI_Bar amount)
    {
        if(info)
        {
            amount.fillAmount = 1;
        }
        else 
        {
            amount.fillAmount = 0;
        }
    }
    private void SetBoolSegment(bool info, GameObject obj)
    {
        obj.SetActive(!info);
    }
}
