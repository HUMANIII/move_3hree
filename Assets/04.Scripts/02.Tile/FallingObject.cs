using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] protected ParticleSystem Effect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameObject.FindGameObjectWithTag("Player")) 
            return;
        
        Effect.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
