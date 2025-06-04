using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.QuantumUI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    public Button UpgradeMaxTimeBtn; 
    public int UpgradeMaxTimePrice;
    public QUI_Bar maxTimeBar;
    public TextMeshProUGUI maxTimeMsg;

    public Button UpgradeBatteryItemBtn; 
    public int UpgradeBatteryItemPrice;
    public QUI_Bar batteryBar;
    public TextMeshProUGUI batteryMsg;

    public Button UpgradeRamItemBtn; 
    public int UpgradeRamItemPrice;
    public QUI_Bar ramBar;
    public TextMeshProUGUI ramMsg;

    public Button UpgradeKnockbackResistBtn; 
    public int UpgradeKnockbackResistPrice;
    public QUI_Bar knockbackBar;

    public Button UpgradeOverclockEfficiencyBtn; 
    public int UpgradeOverclockEfficiencyPrice;
    public QUI_Bar overclockEfficiencyBar;
    public TextMeshProUGUI overclockEfficiencyMsg;

    public Button UpgradeOverclockOptimizationBtn; 
    public int UpgradeOverclockOptimizationPrice;
    public QUI_Bar overclockOptimizationBar;
    public TextMeshProUGUI overclockOptimizationMsg;

    public Button UnlockJailbreakedPhoneBtn; 
    public int UnlockJailbreakedPhonePrice;
    public GameObject jailbreak;

    public Button UnlockGreenApplePhoneBtn; 
    public int UnlockGreenApplePhonePrice;
    public GameObject greenApple;

    public Button UnlockBananaPhoneBtn; 
    public int UnlockBananaPhonePrice;
    public GameObject banana;

    public Button UnlockMithrillPhoneBtn; 
    public int UnlockMithrillPhonePrice;
    public GameObject mithrill;

    public Button confirmBtn; 
    public GameObject confirmWindow;
    public TextMeshProUGUI buyMessageTitle;
    public TextMeshProUGUI buyMessage;

    public TextMeshProUGUI RamCounter1;
    public TextMeshProUGUI RamCounter2;

    private void Awake()
    {
        UpgradeMaxTimeBtn.onClick.AddListener(() => SetButton(UpgradeMaxTime));
        UpgradeBatteryItemBtn.onClick.AddListener(() => SetButton(UpgradeBatteryItem));
        UpgradeRamItemBtn.onClick.AddListener(() => SetButton(UpgradeRamItem));
        UpgradeKnockbackResistBtn.onClick.AddListener(() => SetButton(UpgradeKnockbackResist));
        UpgradeOverclockEfficiencyBtn.onClick.AddListener(() => SetButton(UpgradeOverclockEfficiency));
        UpgradeOverclockOptimizationBtn.onClick.AddListener(() => SetButton(UpgradeOverclockOptimization));
        UnlockJailbreakedPhoneBtn.onClick.AddListener(() => SetButton(UnlockJailbreakedPhone, false));
        UnlockGreenApplePhoneBtn.onClick.AddListener(() => SetButton(UnlockGreenApplePhone, false));
        UnlockBananaPhoneBtn.onClick.AddListener(() => SetButton(UnlockBananaPhone, false));
        UnlockMithrillPhoneBtn.onClick.AddListener(() => SetButton(UnlockMithrillPhone, false));   
    }

    private void Start()
    {
        UpdateState();
    }
    private void CalcValue(ref int value, int raise )
    {
        value *= (int)Mathf.Pow( 2, raise );
    }

    public void SetButton(UnityEngine.Events.UnityAction action, bool IsUpgrade = true)
    {
        confirmWindow.SetActive(true);
        confirmBtn.onClick.AddListener(action);
        buyMessageTitle. text = IsUpgrade ? "업데이트 요청" : "";
        buyMessage.text = IsUpgrade ? "해당 업데이트를 다운로드 하겠습니까?" : "해당 기기를 구매하시겠습니까?";
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

        maxTimeBar.SetFill((psm.maxTimeLimit - psm.upgrade.maxTime) / (float)psm.maxTimeLimit);
        batteryBar.SetFill((psm.batteryItemLimit - psm.upgrade.batteryItem) / (float)psm.batteryItemLimit);
        ramBar.SetFill((psm.ramItemLimit - psm.upgrade.ramItem) / (float)psm.ramItemLimit);
        overclockEfficiencyBar.SetFill((psm.overclockEfficiencyLimit - psm.upgrade.overclockEfficiency) / (float)psm.overclockEfficiencyLimit);
        overclockOptimizationBar.SetFill((psm.overclockOptimizationLimit - psm.upgrade.overclockOptimization) / (float)psm.overclockOptimizationLimit);

        SetBoolSegment(psm.upgrade.knockbackResist == 0, knockbackBar);

        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.JailbreakedPhone) != 0, jailbreak);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.GreenApplePhone) != 0, greenApple);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.BananaPhone) != 0, banana);
        SetBoolSegment((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.MithrillPhone) != 0, mithrill);

        RamCounter1.text = GameManager.Instance.RamCount.ToString();
        RamCounter2.text = GameManager.Instance.RamCount.ToString();
        maxTimeMsg.text = UpgradeMaxTimePrice.ToString();
        batteryMsg.text = UpgradeBatteryItemPrice.ToString();
        ramMsg.text = UpgradeRamItemPrice.ToString();
        overclockEfficiencyMsg.text = UpgradeOverclockEfficiencyPrice.ToString();
        overclockOptimizationMsg.text = UpgradeOverclockOptimizationPrice.ToString();   
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
