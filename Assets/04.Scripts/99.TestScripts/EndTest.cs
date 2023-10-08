using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndTest : MonoBehaviour
{
    public TextMeshProUGUI score;
    //public Button restartButton;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        var gm = GameManager.Instance;
        score.text = $"BestScore : {gm.BestScore} \n Score : {gm.CurScore}";
    }
}
