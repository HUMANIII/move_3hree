using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    protected Collider player;
    protected TileManager tileManager;
    protected int scoreFactor;

    protected virtual void Awake()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        var plyr = GameObject.FindGameObjectWithTag("Player");
        player = plyr.GetComponentInChildren<Collider>();
        scoreFactor = plyr.GetComponent<PlayerController>().scoreFactor;
    }

    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != player) 
            return;
        
        ActiveEffect();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        tileManager.ItemCount--;
    }
    protected abstract void ActiveEffect();
}
