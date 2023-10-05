using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTest : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private TimerScripts timerScripts;

    private void Awake()
    {
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();        
    }
    private void Start()
    {
        bestScoreText.text = "BestScore = " + GameManager.Instance.BestScore.ToString();
    }
    private void Update()
    {
        timerText.text = timerScripts.Timer.ToString();
        scoreText.text = GameManager.Instance.CurScore.ToString();
        if(GameManager.Instance.isGameOver) { bestScoreText.text = "BestScore = " + GameManager.Instance.BestScore.ToString(); }
    }
    
}
