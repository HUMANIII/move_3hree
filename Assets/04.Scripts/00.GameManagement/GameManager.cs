using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SaveDataVC = SaveDataV1;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int BestScore { get; private set; }
    public int CurScore { get; set; } = 0;

    public bool isGameOver = true;
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
    }

    private void Start()
    {
        LoadData();
    }

    public void GameOver()
    {        
        isGameOver = true;
        BestScore = Mathf.Max(BestScore, CurScore);
        SaveData();
    }

    public void GameStart()
    {
        CurScore = 0;
        LoadData();
        isGameOver = false;
    }

    private void SaveData()
    {

        var data = new SaveDataVC();
        data.bestScore = BestScore;
        SaveLoadSystem.Save(data, "saveData.Json");
    }

    private void LoadData()
    {
        var data = SaveLoadSystem.Load("saveData.Json") as SaveDataVC;
        BestScore = data.bestScore;
    }
}
