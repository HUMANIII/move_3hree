using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV6;


public class GameManager : MonoBehaviour
{
    [Flags]
    public enum Settings
    {
        None = 0,
        ControllWithButton = 1 << 0,
        ControllWithScreenTouch = 1 << 1,
    }

    [Flags]
    public enum States
    {
        None = 0,
        IsGameOver = 1 << 0,
        IsPause = 1 << 1,
        IsTrapped = 1 << 2,
        IsHolded = 1 << 3,
        IsLoading = 1 << 4,
    }

    public static GameManager Instance = null;

    public int BestScore { get; private set; }
    public int CurScore { get; set; } = 0;
    public int RamCount { get; set; } = 0;
    public int prevRamCount;

    public Settings Options { get; set; }
    public States State { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.targetFrameRate = 90;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        State &= ~(States.IsGameOver | States.IsTrapped | States.IsPause);
        CurScore = 0;
        LoadData();
        State &= ~(States.IsLoading);        
    }

    public void GameOver()
    {        
        SoundManager.Instance.GameOver();
        State |= States.IsGameOver;
        BestScore = Mathf.Max(BestScore, CurScore);
        SaveData();
    }

    public void GameReStart()
    {
        prevRamCount = RamCount;
        State |= States.IsLoading;
        State &= ~States.IsGameOver;
        State &= ~States.IsTrapped;
        State &= ~States.IsHolded;
        CurScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        State &= ~(States.IsLoading);
    }

    public void SaveData()
    {
        var data = new SaveDataVC
        {
            bestScore = BestScore,
            ramCount = RamCount,
            options = Options,
            upgrade = PlayerStatManager.Instance.upgrade,
            playerType = PlayerStatManager.Instance.playerType,
            soundState = SoundManager.Instance.soundState,
            masterVolume = SoundManager.Instance.masterVolume,
            BGMVolume = SoundManager.Instance.BGMVolume,
            SFXVolume = SoundManager.Instance.SFXVolume,
        };
        SaveLoadSystem.Save(data, "saveData.Json");
    }

    private void LoadData()
    {
        var data = SaveLoadSystem.Load("saveData.Json") as SaveDataVC;
        if(data != null)
        {
            BestScore = data.bestScore;
            RamCount = data.ramCount;
            Options = data.options;
            PlayerStatManager.Instance.upgrade = data.upgrade;
            PlayerStatManager.Instance.ChangePlayerType(data.playerType);
            SoundManager.Instance.soundState = data.soundState;
            SoundManager.Instance.masterVolume = data.masterVolume;
            SoundManager.Instance.BGMVolume = data.BGMVolume;
            SoundManager.Instance.SFXVolume = data.SFXVolume;
        }
        /*
        else
        {
            BestScore = 0;
            RamCount = 0;
            Options = Settings.ControllWithButton;
            var upgrade = new PlayerStatManager.Upgrade();
            upgrade.phoneUnlockInfo = PlayerStatManager.PhoneUnlockInfo.DefaultPhone;
            PlayerStatManager.Instance.upgrade = upgrade;
            PlayerStatManager.Instance.ChangePlayerType(PlayerStatManager.PlayerType.DefaultPhone);
            SoundManager.Instance.soundState = 0;
            SoundManager.Instance.masterVolume = 0;
            SoundManager.Instance.BGMVolume = 0;
            SoundManager.Instance.SFXVolume = 0;
        }
        */
    }

    public void TogglePause()
    {
        State ^= States.IsPause;
    }
    public void IsTrapped()
    {
        State |= States.IsTrapped; 
    }

    public void ReleaseTrap()
    {
        State &= ~States.IsTrapped;
    }
    public void IsHolded()
    {
        State |= States.IsHolded; 
    }

    public void ReleaseHold()
    {
        State &= ~States.IsHolded;
    }
}
