using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public enum MoveTo
    {
        Forward,
        Right,
        Left
    }

    protected Rigidbody rb;
    protected Camera mainCam;
    protected TileManager tileManager;
    protected TimerScripts timerScripts;
    protected PlayerStatManager playerStatManager;
    protected Collider cr;
    protected int knockbackCounter = 0;
    
    public int timerDecreaseFactor = 10; 
    public int scoreFactor = 10;

    protected float moveUpperInterval;
    protected float moveSideInterval;

    public int overclockActiveCount = 40;
    protected int overclockActiveCounter;
    public bool overclocked { get; private set;}

    public int moveCount = 1;
    protected int moveCounter = 0;

    protected int warnEarly = 0;

    protected void Awake()
    {
        mainCam = Camera.main;
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
        playerStatManager = GameObject.FindGameObjectWithTag("PlayerStatManager").GetComponent<PlayerStatManager>();
        rb = GetComponent<Rigidbody>();
        cr = GetComponentInChildren<Collider>();
        overclockActiveCount -= playerStatManager.upgrade.overclockOptimization * playerStatManager.overclockOptimizationRate;
        SpecificEffect();
    }
    protected void Start()
    {
        moveUpperInterval = tileManager.UpperInterval;
        moveSideInterval = tileManager.SideInterval;
        knockbackCounter -= playerStatManager.upgrade.knockbackResist;
    }

    protected void FixedUpdate()
    {
        var gm = GameManager.Instance;
        
        if (transform.position.y < -0.5f && (gm.State & GameManager.States.IsGameOver) == 0)
        {
            gm.GameOver();
        }
    }

    protected void Update()
    {
        //cheatCode
        if(Input.GetKeyDown(KeyCode.F8))
        {
            GameManager.Instance.CurScore += 500;
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            tileManager.ActiveOverclock();
        }
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
                if((gm.State & GameManager.States.IsTrapped) != 0) 
                {
                    var ht = TileManager.CheckUnderTile(transform.position) as HoldTile;
                    ht.Struggle();
                }
                else if(tileScript.CanMove)                
                {
                    if(tileScript as TrapTileScript != null) 
                    {
                        gm.IsTrapped();
                    }
                    MovePosition(tileScript.GetPos());                    
                }
            }
        }

        //testCode
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveWithButton(MoveTo.Forward);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveWithButton(MoveTo.Left);
        }
        if (Input.GetKeyDown(KeyCode.D))
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
        {
            GameManager.Instance.CurScore += scoreFactor;
        }
        else
        {
            GameManager.Instance.CurScore += scoreFactor / 2;
        }

        pos.y += 1.8f;
        var ts = TileManager.CheckUnderTile(pos);
        if(ts != null) 
        {
            ts.GetPos();
            if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
                pos.y -= 1.4f;
        }

        if((timerScripts.curMaxTime / 2f) < timerScripts.Timer)
        {
            overclockActiveCounter++;
        }
        else
        {
            overclockActiveCounter = 0;
        }
        Debug.Log(overclockActiveCounter);
        MoveObjectAndTriggerEvent(pos);

        if (overclockActiveCounter >= overclockActiveCount) 
        {
            tileManager.ActiveOverclock();
            overclockActiveCounter = 0;
        }
        tileManager.SpawnTile();
        tileManager.CheckAllTiles();

        if (tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
        {
            timerScripts.DecreaseMaxTime();
        }
        moveCounter++;
        if(moveCount <= moveCounter)
        {
            timerScripts.ResetTimer();
            moveCounter = 0;
        }
    }

    public void MoveWithButton(MoveTo where) 
    {
        var gm = GameManager.Instance;
        if ((gm.Options & GameManager.Settings.ControllWithButton) == 0)
            return;

        Vector3 pos = transform.position;
        if ((gm.State & GameManager.States.IsTrapped) != 0)
        {
            var ht = TileManager.CheckUnderTile(pos) as HoldTile;
            if(ht != null)
            {
                ht.Struggle();
            }
            return;
        }

        pos += where switch
        {
            MoveTo.Forward => new Vector3(0f, 0f, moveUpperInterval * 2),
            MoveTo.Left => new Vector3(-moveSideInterval * 2, 0f, moveUpperInterval),
            MoveTo.Right => new Vector3(moveSideInterval * 2, 0f, moveUpperInterval),           
            _ => new Vector3()
        };
        MovePosition(pos);
        var ts = TileManager.CheckUnderTile(transform.position);
        if (ts != null)         
        {
            ts.GetPos();
            if(ts.GetComponent<TrapTileScript>() != null)
            {
                GameManager.Instance.IsTrapped();                
            }
            if (ts.gameObject.GetComponent<KnockbackTile>() != null)
            {
                GameManager.Instance.ReleaseTrap();
            }
        }
    }

    public void Knockback()
    {
        var kc = Mathf.Clamp(knockbackCounter, 0, 1);
        var knockbackRange = moveUpperInterval * (kc + 1) * 2;
        var pos = transform.position;
        pos.z -= knockbackRange;
        MoveObjectAndTriggerEvent(pos);
        //tileManager.CheckAllTiles();
        knockbackCounter++;
    }

    void MoveObjectAndTriggerEvent(Vector3 newPosition)
    {
        transform.position = newPosition;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.8f);
        foreach (Collider collider in colliders)
        {
            var kb = collider.GetComponent<KnockbackTile>();
            if(kb != null)
            {
                Knockback();
            }
        }
    }

    protected virtual void SpecificEffect()
    {

    }
}
