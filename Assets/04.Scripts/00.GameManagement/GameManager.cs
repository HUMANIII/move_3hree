using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataVC = SaveDataV2;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int BestScore { get; private set; }
    public int CurScore { get; set; } = 0;
    public int RamCount { get; set; } = 0;

    public bool IsGameOver { get; private set; } = false;
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
        IsGameOver = false;
        CurScore = 0;
        LoadData();
    }

    public void GameOver()
    {        
        IsGameOver = true;
        BestScore = Mathf.Max(BestScore, CurScore);
        SaveData();
    }

    public void GameReStart()
    {
        IsGameOver = false;
        CurScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SaveData()
    {
        var data = new SaveDataVC();
        data.bestScore = BestScore;
        data.ramCount = RamCount;
        SaveLoadSystem.Save(data, "saveData.Json");
    }

    private void LoadData()
    {
        var data = SaveLoadSystem.Load("saveData.Json") as SaveDataVC;
        if(data != null)
        {
            BestScore = data.bestScore;
            RamCount = data.ramCount;
        }
    }
}
