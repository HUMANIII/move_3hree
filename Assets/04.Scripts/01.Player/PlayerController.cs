using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum MoveTo
    {
        Forward,
        Right,
        Left
    }

    private Camera mainCam;
    private TileManager tileManager;
    private TimerScripts timerScripts;
    private int knockbackCounter = 0;
    
    public int timerDecreaseFactor = 10; 
    public int scoreFactor = 10;

    private float moveUpperInterval;
    private float moveSideInterval;

    private void Awake()
    {
        mainCam = Camera.main;
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
    }
    private void Start()
    {
        moveUpperInterval = tileManager.upperInterval;
        moveSideInterval = tileManager.sideInterval;
    }

    private void FixedUpdate()
    {
        var gm = GameManager.Instance;

        if (transform.position.y < -0.5f && !gm.IsGameOver)
        {
            gm.GameOver();
        }
    }

    protected void Update()
    {
        var gm = GameManager.Instance;
        if ((gm.State & (GameManager.States.IsGameOver | GameManager.States.IsPause)) != 0)
            return;        

        if(((gm.Options & GameManager.Settings.ControllWithButton) == 0) && Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            TileScript tileScript = null;

            if (Physics.Raycast(ray, out RaycastHit hit, 999f))
                hit.collider.TryGetComponent(out tileScript);

            if (tileScript != null)
            {
                if(tileScript.CanMove)                
                {
                    MovePosition(tileScript.GetPos());                    
                }
            }
        }

        //testCode
        if(Input.GetKeyDown(KeyCode.W)) 
        { 
            MoveWithButton(MoveTo.Forward); 
        }
        if(Input.GetKeyDown(KeyCode.A)) 
        {
            MoveWithButton(MoveTo.Left); 
        }
        if(Input.GetKeyDown(KeyCode.D)) 
        {
            MoveWithButton(MoveTo.Right); 
        }
    }
    public void MovePosition(Vector3 pos)
    {
        var gm = GameManager.Instance;
        if ((gm.State & GameManager.States.IsTrapped) != 0)
            return;

        if (pos.x == transform.position.x)
            GameManager.Instance.CurScore += scoreFactor;
        else
            GameManager.Instance.CurScore += scoreFactor / 2;

        pos.y += 1.8f;
        transform.position = pos;
        tileManager.SpawnTile();
        tileManager.CheckAllTiles(); 

        if (tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
        {
            timerScripts.DecreaseMaxTime();
        }
        timerScripts.ResetTimer();
    }

    public void MoveWithButton(MoveTo where) 
    {
        if((GameManager.Instance.Options & GameManager.Settings.ControllWithButton) == 0)
            return;
        if ((GameManager.Instance.State & GameManager.States.IsTrapped) != 0)
            return;

        Vector3 pos = where switch
        {
            MoveTo.Forward => new Vector3(0f, 0f, moveUpperInterval * 2),
            MoveTo.Left => new Vector3(-moveSideInterval * 2, 0f, moveUpperInterval),
            MoveTo.Right => new Vector3(moveSideInterval * 2, 0f, moveUpperInterval),            
        };
        pos += transform.position;
        //var tiles = GameObject.FindGameObjectsWithTag("Tile");
        //GameObject nearestTile = null;
        //float distance = float.MaxValue;
        //foreach (var tile in tiles)
        //{
        //    var tileDistance = Vector3.Distance(tile.transform.position, pos);
        //    if(distance > tileDistance)
        //    {
        //        distance = tileDistance; 
        //        nearestTile = tile;
        //    }
        //}
        var ts = CheckUnderTile(pos);
        if (ts == null) 
        {
            pos.y += 1.8f;
            transform.position = pos;
        }
        else
        {
            MovePosition(ts.GetPos());
            if(ts.GetComponent<TrapTileScript>() != null)
            {
                GameManager.Instance.IsTrapped();
            }
        }
    }

    public void Knockback()
    {
        var knockbackRange = moveUpperInterval * (knockbackCounter + 1) * 2;
        var pos = transform.position;
        pos.z -= knockbackRange;
        transform.position = pos;
        tileManager.CheckAllTiles();
        knockbackCounter++;
    }        

    private TileScript CheckUnderTile(Vector3 pos)
    {
        Physics.Raycast(pos, Vector3.down, out var hitInfo,10f);
        if (hitInfo.collider == null)
        {
            GameManager.Instance.IsTrapped();
            return null;
        }
        return hitInfo.collider.GetComponent<TileScript>();
    }
}
