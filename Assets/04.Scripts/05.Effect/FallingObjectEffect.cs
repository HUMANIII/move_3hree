using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float timer;
    private float delTime;
    public float timerInterval = 1f;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();        
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (delTime > timer)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnEnable()
    {
        timer = Time.time;
        delTime = Time.time + timerInterval;
    }
}
