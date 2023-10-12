using System;
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
        var ps = PlayerStatManager.Instance;

        maxTime += ps.maxTimeRate * ps.upgrade.maxTime;
        curMaxTime = maxTime;
        ResetTimer();
    }
    private void FixedUpdate()                    
    {
        if ((GameManager.Instance.State & (GameManager.States.IsGameOver | GameManager.States.IsPause)) != 0)
            return;

        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Knockback();
            ResetTimer();
        }
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
    }
}
