using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV3;


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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        State &= ~(States.IsGameOver | States.IsTrapped);
        CurScore = 0;
        LoadData();
    }

    public void GameOver()
    {        
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

    private void SaveData()
    {
        var data = new SaveDataVC
        {
            bestScore = BestScore,
            ramCount = RamCount,
            options = Options,
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
