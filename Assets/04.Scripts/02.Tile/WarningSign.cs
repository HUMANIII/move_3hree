using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WarningSign : MonoBehaviour
{
    public GameObject fallingObj;

    private GameObject player;
    private Rigidbody rb;
    private Collider cldr;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        cldr = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        var objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in objs)
        {
            if (other.gameObject == obj)
            {
                fallingObj.SetActive(true);
            }
        }
    }
}
