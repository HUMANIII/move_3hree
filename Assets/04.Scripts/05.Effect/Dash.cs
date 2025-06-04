using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
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
        if (delTime < timer)
        {
            gameObject.SetActive(false);
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
