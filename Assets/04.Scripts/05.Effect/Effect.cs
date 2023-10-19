using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectEffect : MonoBehaviour
{
    private ParticleSystem effect;
    private float timer;
    private float delTime;
    public float timerInterval = 1f;
    private void Awake()
    {
        effect = GetComponent<ParticleSystem>();        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (delTime < timer &&((GameManager.Instance.State & GameManager.States.IsGameOver) == 0))
        {
            GameManager.Instance.GameOver();
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    private void OnEnable()
    {
        effect.Stop();
        effect.Play();
        timer = Time.time;
        delTime = Time.time + timerInterval;
    }
}
