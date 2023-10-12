using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerStatManager;

public class PlayerStatManager : MonoBehaviour
{
    [Flags]
    public enum PhoneUnlockInfo
    {
        DefaultPhone = 1,
        JailbreakedPhone = 2,
        GreenApplePhone = 4,
        BananaPhone = 8,
        MithrillPhone = 16,
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
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            testCounter++;
            if(testCounter == playerList.Count)
            {
                testCounter = 0;
            }
            ChangePlayerType(testCounter);
        }
    }

    public void ChangePlayerType(int type)
    {
        Debug.Log("PlayerChanged");
        player = playerList[type];
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            Instantiate(player,new Vector3(0f, 1.7f, 0f),Quaternion.identity);
        }
    }

    public bool UpGradeMaxTime()
    {
        if(upgrade.maxTime != maxTimeLimit)
        {
            upgrade.maxTime++;
            GameManager.Instance.SaveData();
            return true;
        }        
        return false;        
    }
    public bool UpGradeBatteryItem()
    {
        if(upgrade.batteryItem != batteryItemLimit) 
        {
            upgrade.batteryItem++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpGradeRamItem()
    {
        if(upgrade.ramItem != ramItemLimit)
        {
            upgrade.ramItem++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpGradeKnockbackResist()
    {
        if(upgrade.knockbackResist != knockbackResistLimit)
        {
            upgrade.knockbackResist++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpGradeOverclockEfficiency()
    {
        if(upgrade.overclockEfficiency != overclockEfficiencyLimit)
        {
            upgrade.overclockEfficiency++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }
    public bool UpGradeOverclockOptimization()
    {
        if(upgrade.overclockOptimization != overclockOptimizationLimit)
        {
            upgrade.overclockOptimization++;
            GameManager.Instance.SaveData();
            return true;
        }
        return false;
    }

}
