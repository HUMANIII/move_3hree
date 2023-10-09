using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerTest : MonoBehaviour
{
    public Button pauseBtn;
    public Slider timerSlier;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private TimerScripts timerScripts;
    public GameObject endPannel;    
    public GameObject pausePage;    
    public GameObject moveButtons;    

    private void Awake()
    {
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
    }
    private void Start()
    {
        var gm = GameManager.Instance;
        pauseBtn.onClick.AddListener(Pause);
        //endPannel = GameObject.FindGameObjectWithTag("Pannel");
        bestScoreText.text = "BestScore = " + gm.BestScore.ToString();
        if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
        {
            moveButtons.SetActive(true);
        }
        else
        {
            moveButtons.SetActive(false);
        }
    }
    private void Update()
    {
        timerSlier.maxValue = TimerScripts.curMaxTime;
        timerSlier.value = timerScripts.Timer;
        scoreText.text = GameManager.Instance.CurScore.ToString();
        if(GameManager.Instance.IsGameOver) 
        {
            endPannel.SetActive(true);
            bestScoreText.text = "BestScore = " + GameManager.Instance.BestScore.ToString();
        }
    }    

    private void Pause()
    {
        pausePage.SetActive(true);
        GameManager.Instance.TogglePause();
    }

    public void ToggleMoveOpt()
    {
        var gm = GameManager.Instance;
        gm.ToggleMoveOption();
        if((gm.Options & GameManager.Settings.ControllWithButton) != 0)
        {
            moveButtons.SetActive(true);
        }
        else
        {
            moveButtons.SetActive(false);
        }
    }
}
