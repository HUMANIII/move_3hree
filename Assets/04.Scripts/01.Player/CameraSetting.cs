using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetting : MonoBehaviour
{
    private CinemachineVirtualCamera vc;

    private void Awake()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        vc.Follow = player;
        //vc.LookAt = player;
    }
}
