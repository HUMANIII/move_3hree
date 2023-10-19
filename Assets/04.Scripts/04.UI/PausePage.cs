using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : MonoBehaviour
{
    public Button resume;
    public Button mainMenu;
    public Button exit;

    private void Awake()
    {
        resume.onClick.AddListener(Resume);
        mainMenu.onClick.AddListener(MainMenu);
        exit.onClick.AddListener(Exit);
        gameObject.SetActive(false);
    }

    private void Resume()
    {
        SoundManager.Instance.ClickSound();
        gameObject.SetActive(false);
        GameManager.Instance.TogglePause();
    }

    public void MainMenu()
    {
        UIManager.Instance.OnMainMenuButton();
        GameManager.Instance.TogglePause();
    }

    public void Exit()
    {
        UIManager.Instance.OnExitButton();
    }
}
