using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Button pauseBtn;
    public Slider timerSlier;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ramText;
    private TimerScripts timerScripts;
    public GameObject endPannel;    
    public GameObject pausePage;     
    
    public GameObject ButtonController;
    public GameObject ScreenController;

    private void Awake()
    {
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
    }
    private void Start()
    {
        var gm = GameManager.Instance;
        pauseBtn.onClick.AddListener(Pause);
        SetController();
        //endPannel = GameObject.FindGameObjectWithTag("Pannel");
        //bestScoreText.text = "BestScore = " + gm.BestScore.ToString();
        //moveButtons.SetActive(true);
        //if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
        //{
        //    moveButtons.SetActive(true);
        //}
        //else
        //{
        //    moveButtons.SetActive(false);
        //}
    }
    private void Update()
    {
        var gm = GameManager.Instance;
        timerSlier.maxValue = timerScripts.curMaxTime;
        timerSlier.value = timerScripts.Timer;
        scoreText.text = $"{gm.CurScore}M";       
        ramText.text = (gm.RamCount - gm.prevRamCount).ToString();

        if((gm.State & GameManager.States.IsGameOver) != 0)
        {
            endPannel.SetActive(true);
            //bestScoreText.text = "BestScore = " + GameManager.Instance.BestScore.ToString();
        }
    }    

    private void Pause()
    {
        SoundManager.Instance.ClickSound();
        //pausePage.gameObject.SetActive(true);
        GameManager.Instance.TogglePause();
    }

    public void SetController()
    {
        var gm = GameManager.Instance.Options;
        ButtonController.SetActive((gm & GameManager.Settings.ControllWithButton) != 0);
        ScreenController.SetActive((gm & GameManager.Settings.ControllWithScreenTouch) != 0);
    }

}
