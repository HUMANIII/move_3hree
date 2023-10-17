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
        var go = collision.gameObject;
        if (go == player || go == fallingObj)
        {
            cldr.isTrigger = true;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            //GameManager.Instance.GameOver();
            fallingObj.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            
        }
    }
}
