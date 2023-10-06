using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    private GameObject Tutopopup;
    private GameObject Settingpopup;
    private GameObject Shoppopup;

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
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var Panel = GameObject.FindGameObjectWithTag("Pannel");

        Tutopopup = Panel.transform.GetChild(2).gameObject;
        Settingpopup = Panel.transform.GetChild(3).gameObject;
        Shoppopup = Panel.transform.GetChild(4).gameObject;
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnShopButton()
    {
        Shoppopup.SetActive(true);
    }

    public void CloseShoppopup()
    {
        Shoppopup.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnTutoPopup()
    {
        Tutopopup.SetActive(true);
    }

    public void CloseTutoPopup()
    {
        Tutopopup.SetActive(false);
    }

    public void OnSettingPopup()
    {
        Settingpopup.SetActive(true);
    }

    public void CloseSettingPopup()
    {
        Settingpopup.SetActive(false);
    }

}
