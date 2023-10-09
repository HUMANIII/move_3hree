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
        IsGameOver = 1,
        IsPause = 2,
        ControllWithButton = 4
    }

    public static GameManager Instance = null;

    public int BestScore { get; private set; }
    public int CurScore { get; set; } = 0;
    public int RamCount { get; set; } = 0;

    public Settings Options { get; private set; }

    public bool IsGameOver { get; private set; } = false;
    public bool IsPause { get; private set; } = false;
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
        Options &= ~Settings.IsGameOver;
        IsGameOver = false;
        CurScore = 0;
        LoadData();
    }

    public void GameOver()
    {        
        IsGameOver = true;
        Options |= Settings.IsGameOver;
        BestScore = Mathf.Max(BestScore, CurScore);
        SaveData();
    }

    public void GameReStart()
    {
        IsGameOver = false;
        Options &= ~Settings.IsGameOver;
        CurScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveData()
    {
        var data = new SaveDataVC
        {
            bestScore = BestScore,
            ramCount = RamCount,
            options = Options & ~(Settings.IsPause | Settings.IsGameOver)
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
        Options ^= Settings.IsPause;
    }

    public void Resume()
    {
        Options &= ~Settings.IsPause;
        IsPause = false;
    }

    public void ToggleMoveOption()
    {
        Options ^= Settings.ControllWithButton;        
    }
}
