using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndTest : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Button restartButton;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Start()
    {
        restartButton.onClick.AddListener(GameManager.Instance.GameReStart);        
    }
    private void OnEnable()
    {
        score.text = $"BestScore : {GameManager.Instance.BestScore} \n Score : {GameManager.Instance.CurScore}";
    }
}
