using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUISetting : MonoBehaviour
{
    public Button homeButton;
    public Button restartButton;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var uiMgr = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        homeButton.onClick.AddListener(uiMgr.OnMainMenuButton);
        restartButton.onClick.AddListener(uiMgr.OnStartButton);
    }
}
