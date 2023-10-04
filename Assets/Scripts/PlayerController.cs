using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    protected void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);
            TileScript tileScript = hit.collider.GetComponent<TileScript>();

            if (tileScript != null)
            {
                Debug.Log(tileScript.CanMove);
                if(tileScript.CanMove)                
                {
                    //Debug.Log(tileScript.GetPos());
                    MovePosition(tileScript.GetPos());
                    if(tileManager.PlayerLineCounter % timerDecreaseFactor == 0)
                    {
                        timerScripts.DecreaseMaxTime();
                    }
                    timerScripts.ResetTimer();
                }
            }
        }        
    }
    public void MovePosition(Vector3 pos)
    {
        pos.y += 1.8f;
        transform.position = pos;
        tileManager.SpawnTile();
    }
}
