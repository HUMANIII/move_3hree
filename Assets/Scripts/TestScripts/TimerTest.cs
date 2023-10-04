using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    private TimerScripts timerScripts;

    private void Awake()
    {
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
    }
    private void Update()
    {
        text.text = timerScripts.Timer.ToString();
    }
}
