using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject Tutopopup;
    public GameObject Settingpopup;
    public GameObject Shoppopup;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("MainGameScene");
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
