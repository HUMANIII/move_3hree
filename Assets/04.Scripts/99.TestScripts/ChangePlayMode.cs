using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayMode : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(GameManager.Instance.ToggleMoveOption);
    }

    public void ChangeMoveMode()        
    {
        var gm = GameManager.Instance;
        gm.ToggleMoveOption();
        if ((gm.Options & GameManager.Settings.ControllWithButton) != 0)
        {
            //moveButtons.SetActive(true);
        }
        else
        {
            //moveButtons.SetActive(false);
        }
    }
}
