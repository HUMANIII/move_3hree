using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    protected List<GameObject> nearTiles = new();
    protected GameObject player;

    public int lineNumber { get; set; } = 0;
    protected TileManager tileManager;

    protected float playerSearchFactor;
    protected float playerSearchRange = 2f;

    public bool CanMove { get; set; } = false;

    private void Awake()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSearchFactor = tileManager.sizeFator;        
    }

    public void Init (int LineNum)
    {
        lineNumber = LineNum;
    }

    public virtual Vector3 GetPos()
    {
        tileManager.PlayerLineCounter = lineNumber;
        return gameObject.transform.position;
    }

    public virtual void CheckTile()
    {
        if (player.transform.position.z >= transform.position.z)
        {
            CanMove = false;
        }
        else
        {
            CheckPlayerRange();
        }

        if (tileManager.PlayerLineCounter - lineNumber > 1)
        {
            tileManager.destroyTiles.Add(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void CheckPlayerRange()
    {
        float range = Vector3.Distance(transform.position, player.transform.position);
        CanMove = range < playerSearchRange * playerSearchFactor;
        
    }
}
