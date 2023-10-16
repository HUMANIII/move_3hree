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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.GameReStart();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        homeButton.onClick.AddListener(UIManager.Instance.OnMainMenuButton);
        restartButton.onClick.AddListener(GameManager.Instance.GameReStart);
    }
}