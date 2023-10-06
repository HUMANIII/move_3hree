using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemScript : MonoBehaviour
{
    protected GameObject player;
    protected TileManager tileManager;
    protected int scoreFactor;

    private void Awake()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        scoreFactor = player.GetComponent<PlayerController>().scoreFactor;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            ActiveEffect();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        tileManager.ItemCount--;
    }
    protected abstract void ActiveEffect();
}
