using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Flags]
    enum MoveTo
    {
        Forward,
        Right,
        Left
    }

    private MoveTo moveTo;

    private Camera mainCam;
    private TileManager tileManager;
    private TimerScripts timerScripts;
    
    public int timerDecreaseFactor = 10;

    private void Awake()
    {
        mainCam = Camera.main;
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        timerScripts = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScripts>();
    }
    private void FixedUpdate()
    {
        
    }
    protected void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            TileScript tileScript = null;

            if (Physics.Raycast(ray, out RaycastHit hit, 999f))
                hit.collider.TryGetComponent<TileScript>(out tileScript);

            if (tileScript != null)
            {
                Debug.Log(tileScript.CanMove);
                if(tileScript.CanMove)                
                {
                    MovePosition(tileScript.GetPos());
                    if(tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
                    {
                        timerScripts.DecreaseMaxTime();
                    }
                    timerScripts.ResetTimer();
                }
            }
        }        

        if(Input.GetKeyDown(KeyCode.W)) { }
        if(Input.GetKeyDown(KeyCode.A)) { }
        if(Input.GetKeyDown(KeyCode.D)) { }
    }
    public void MovePosition(Vector3 pos)
    {
        pos.y += 1.8f;
        transform.position = pos;
        tileManager.SpawnTile();
    }
}
