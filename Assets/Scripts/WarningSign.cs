using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    public GameObject fallingObj;

    private GameObject player;
    private Rigidbody rb;
    private Collider collider;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var go = collision.gameObject;
        if (go == player || go == fallingObj)
        {
            collider.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
