using UnityEngine;
using static GameManager.States;

public class TimerScripts : MonoBehaviour
{
    public float Timer { get; set; }
    public float curMaxTime;

    
    public float maxTime = 2f;
    public float minTime = 0.6f;
    public float decreaseFactor = 0.1f;

    private PlayerStatManager.PlayerType playerType;
    private PlayerController playerController;

    private void Start()
    {
        var ps = PlayerStatManager.Instance;
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxTime += ps.maxTimeRate * ps.upgrade.maxTime - playerController.maxTimeLimitCtrl;
        curMaxTime = maxTime;
        playerType = PlayerStatManager.Instance.playerType;
        ResetTimer();
        if (playerType == PlayerStatManager.PlayerType.BananaPhone)
        {
            curMaxTime = 120f;
            Timer = curMaxTime;
        }
    }
    private void FixedUpdate()                    
    {
        if ((GameManager.Instance.State & (IsGameOver | IsPause | IsTrapped)) != 0)
            return;

        Timer -= Time.deltaTime;        

        if (Timer <= 0)
        {
            if (playerType != PlayerStatManager.PlayerType.BananaPhone)
            {
                playerController.Knockback();
                ResetTimer();
            }
            else
            {
                GameManager.Instance.GameOver();
            }
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

    public void RestoreTime(float amount)
    {
        if (playerType != PlayerStatManager.PlayerType.BananaPhone)
        {
            curMaxTime = Mathf.Clamp(curMaxTime + amount, minTime, maxTime);
        }
        else
        {
            Timer += amount;
        }
    }

}
