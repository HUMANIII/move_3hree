using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScripts : MonoBehaviour
{
    public float Timer { get; set; }
    public static float curMaxTime;

    public static float maxTime = 2f;
    public static float minTime = 0.6f;
    public float decreaseFactor = 0.1f;

    private void Start()
    {
        curMaxTime = maxTime;
        ResetTimer();
    }
    private void FixedUpdate()                    
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
            GameManager.Instance.GameOver();
    }

    public void ResetTimer()
    {
        Timer = curMaxTime;
    }

    public void DecreaseMaxTime()
    {
        curMaxTime = Mathf.Clamp(curMaxTime - decreaseFactor, minTime, maxTime);
    }

    public static void RestoreTime(float amount)
    {
        curMaxTime = Mathf.Clamp(curMaxTime + amount, minTime, maxTime);
        Debug.Log(curMaxTime);
    }
}
