using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV5;


public class GameManager : MonoBehaviour
{
    [Flags]
    public enum Settings
    {
        None = 0,
        ControllWithButton = 1
    }

    [Flags]
    public enum States
    {
        None = 0,
        IsGameOver = 1,
        IsPause = 2,
        IsTrapped = 4,
    }

    public static GameManager Instance = null;

    public int BestScore { get; private set; }
    public int CurScore { get; set; } = 0;
    public int RamCount { get; set; } = 0;

    public Settings Options { get; private set; }
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
        State &= ~States.IsGameOver;
        State &= ~States.IsTrapped;
        CurScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        }
        else
        {
            BestScore = 0;
            RamCount = 0;
            Options = new();
            PlayerStatManager.Instance.upgrade = new();
            PlayerStatManager.Instance.ChangePlayerType(PlayerStatManager.PlayerType.DefaultPhone);
        }
    }

    public void TogglePause()
    {
        State ^= States.IsPause;
    }

    public void ToggleMoveOption()
    {
        Options ^= Settings.ControllWithButton;        
    }
    public void IsTrapped()
    {
        State |= States.IsTrapped; 
    }

    public void ReleaseTrap()
    {
        State &= ~States.IsTrapped;
    }
}
