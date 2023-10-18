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
        if(Panel != null)
        {
            Tutopopup = Panel.transform.GetChild(4).gameObject;
            Settingpopup = Panel.transform.GetChild(5).gameObject;
            Shoppopup = Panel.transform.GetChild(6).gameObject;
        }

    }

    public void OnMainMenuButton()
    {
        SoundManager.Instance.ClickSound();
        SoundManager.Instance.MoveToMainMenu();
        SceneManager.LoadScene("MainMenu 1");
    }
    
    public void OnStartButton()
    {
        SoundManager.Instance.ClickSound();
        var gm = GameManager.Instance;
        if(gm != null)
        {
            gm.GameReStart();
        }
        SceneManager.LoadScene("GameScene");
    }

    public void OnShopButton()
    {
        SoundManager.Instance.ClickSound();
        Shoppopup.SetActive(true);
    }

    public void CloseShoppopup()
    {
        SoundManager.Instance.ClickSound();
        Shoppopup.SetActive(false);
    }

    public void OnExitButton()
    {
        SoundManager.Instance.ClickSound();
        Application.Quit();
    }

    public void OnTutoPopup()
    {
        SoundManager.Instance.ClickSound();
        Tutopopup.SetActive(true);
    }

    public void CloseTutoPopup()
    {
        SoundManager.Instance.ClickSound();
        Tutopopup.SetActive(false);
    }

    public void OnSettingPopup()
    {
        SoundManager.Instance.ClickSound(); 
        Settingpopup.SetActive(true);
    }

    public void CloseSettingPopup()
    {
        SoundManager.Instance.ClickSound();
        Settingpopup.SetActive(false);
    }


}
