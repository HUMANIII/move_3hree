using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePage : MonoBehaviour
{
    public Button resume;

    private void Awake()
    {
        resume.onClick.AddListener(Resume);
        gameObject.SetActive(false);
    }

    private void Resume()
    {
        gameObject.SetActive(false);
        GameManager.Instance.TogglePause();
    }
}
