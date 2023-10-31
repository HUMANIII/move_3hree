using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStatManager : MonoBehaviour
{
    [Flags]
    public enum PhoneUnlockInfo
    {
        DefaultPhone = 1 << 0,
        JailbreakedPhone = 1 << 1,
        GreenApplePhone = 1 << 2,
        BananaPhone = 1 << 3,
        MithrillPhone = 1 << 4,
    }        
    public enum PlayerType
    {
        DefaultPhone,
        JailbreakedPhone,
        GreenApplePhone,
        BananaPhone,
        MithrillPhone, 
        Nums
    }

    public class Upgrade
    {
        public int maxTime;
        public int batteryItem;
        public int ramItem;
        public int knockbackResist;
        public int overclockEfficiency;
        public int overclockOptimization;
        public PhoneUnlockInfo phoneUnlockInfo;
    };

    
    public static PlayerStatManager Instance;

    private GameObject player;

    public List<GameObject> playerList;

    public Upgrade upgrade;
    public PlayerType playerType;

    public int maxTimeLimit = 4;
    public float maxTimeRate = 0.05f;
    public int batteryItemLimit = 4;
    public float batteryItemRate = 0.05f;
    public int ramItemLimit = 4;
    public int ramItemRate = 1;
    public int knockbackResistLimit = 1;
    public int overclockEfficiencyLimit = 4;
    public int overclockEfficiencyRate = 1;
    public int overclockOptimizationLimit = 4;
    public int overclockOptimizationRate = 5;

    private int testCounter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if(player == null)
        {
            player = playerList[0];
            playerType = PlayerType.DefaultPhone;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            TestMethod();
        }
    }

    public void TestMethod()
    {
        testCounter++;
        if (testCounter == playerList.Count)
        {
            testCounter = 0;
        }
        ChangePlayerType(testCounter);
    }

    public void ChangePlayerType(int type)
    {
       ChangePlayerType((PlayerType)type); 
    }
    public void ChangePlayerType(PlayerType type)
    {
        if (playerType != type)
        {
            playerType = type;
            player = playerList[(int)type];
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            Instantiate(player,new Vector3(0f, 1.7f, 0f),Quaternion.Euler(0f,180f,0f));
        } 
    }

    public bool UpgradeMaxTime()
    {
        if(upgrade.maxTime != maxTimeLimit)
        {
            upgrade.maxTime++;
            GameManager.Instance.SaveData();
            return true;
        }        
        return false;        
    }
    public bool UpgradeBatteryItem()
    {
        if(upgrade.batteryItem != batteryItemLimit) 
        {
            upgrade.batteryItem++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpgradeRamItem()
    {
        if(upgrade.ramItem != ramItemLimit)
        {
            upgrade.ramItem++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpgradeKnockbackResist()
    {
        if(upgrade.knockbackResist != knockbackResistLimit)
        {
            upgrade.knockbackResist++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpgradeOverclockEfficiency()
    {
        if(upgrade.overclockEfficiency != overclockEfficiencyLimit)
        {
            upgrade.overclockEfficiency++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpgradeOverclockOptimization()
    {
        if(upgrade.overclockOptimization != overclockOptimizationLimit)
        {
            upgrade.overclockOptimization++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }

    public bool UnlockJailbreakedPhone()
    {
        if((upgrade.phoneUnlockInfo & PhoneUnlockInfo.JailbreakedPhone) != 0)
            return false;
        upgrade.phoneUnlockInfo |= PhoneUnlockInfo.JailbreakedPhone;
        GameManager.Instance.SaveData();
        return true;
    }
    public bool UnlockGreenApplePhone()
    {
        if((upgrade.phoneUnlockInfo & PhoneUnlockInfo.GreenApplePhone) != 0)
            return false;
        upgrade.phoneUnlockInfo |= PhoneUnlockInfo.GreenApplePhone;
        GameManager.Instance.SaveData();
        return true;
    }
    public bool UnlockBananaPhone()
    {
        if((upgrade.phoneUnlockInfo & PhoneUnlockInfo.BananaPhone) != 0)
            return false;
        upgrade.phoneUnlockInfo |= PhoneUnlockInfo.BananaPhone;
        GameManager.Instance.SaveData();
        return true;
    }
    public bool UnlockMithrillPhone()
    {
        if((upgrade.phoneUnlockInfo & PhoneUnlockInfo.MithrillPhone) != 0)
            return false;
        upgrade.phoneUnlockInfo |= PhoneUnlockInfo.MithrillPhone;
        GameManager.Instance.SaveData();
        return true;
    }
}
