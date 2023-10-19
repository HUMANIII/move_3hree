using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public TextMeshProUGUI ramGot;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI currentScore;

    public Button restartButton;
    public Button mainMenuButton;

    private void Awake()
    {
        gameObject.SetActive(false);
        restartButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void OnEnable()
    {
        var gm = GameManager.Instance;
        ramGot.text = $"여유 메모리 : {gm.RamCount - gm.prevRamCount}MB";
        bestScore.text = $"최고 이동 거리 : {gm.BestScore}M";
        currentScore.text = $"이동 거리 : {gm.CurScore}M";
    }

    public void Restart()
    {
        UIManager.Instance.OnStartButton();
    }

    public void MainMenu()
    {
        UIManager.Instance.OnMainMenuButton();
    }
}
