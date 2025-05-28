using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTile : TrapTileScript
{
    [SerializeField] private List<ParticleSystem> effects;

    private void OnTriggerEnter(Collider other)
    {
        var objs = GameObject.FindGameObjectsWithTag("Player");
        foreach (var obj in objs)
        {
            if (other.gameObject != obj) 
                continue;
            
            var index = (int)PlayerStatManager.Instance.playerType;
            var pos = transform.position;
            pos.y += 0.5f;
            Instantiate(effects[index], pos, Quaternion.Euler(-90f, 0f, 0f));
        }
    }
}
