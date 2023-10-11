using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    private GameObject player;

    public List<GameObject> playerList;

    private int testCounter = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        if(player == null)
        {
            player = playerList[0];
        }        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            testCounter++;
            if(testCounter == playerList.Count)
            {
                testCounter = 0;
            }
            ChangePlayerType(testCounter);
        }
    }

    public void ChangePlayerType(int type)
    {
        Debug.Log("PlayerChanged");
        player = playerList[type];
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            Instantiate(player,new Vector3(0f, 1.7f, 0f),Quaternion.identity);
        }
    }
}
