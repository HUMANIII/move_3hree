using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownController : MonoBehaviour
{
    TMP_Dropdown options;

    List<string> optionList = new List<string>();

    void Start()
    {
        options = GetComponent<TMP_Dropdown>();

        options.ClearOptions();

        optionList.Add("버튼 터치");
        optionList.Add("화면 터치");

        options.AddOptions(optionList);

        if((GameManager.Instance.Options & GameManager.Settings.ControllWithButton) != 0)
        {
            options.value = 0;
        }
        else
        {
            options.value = 1;
        }

        //options.onValueChanged.AddListener(delegate { OnValueChanged(options.value); });
    }
    public void OnValueChanged(int value)
    {
        var opt = GameManager.Instance.Options;
        switch (value)
        {
            case 0:
                opt |= GameManager.Settings.ControllWithButton;
                opt &= ~GameManager.Settings.ControllWithScreenTouch;
                break;
            case 1:
                opt &= ~GameManager.Settings.ControllWithButton;
                opt |= GameManager.Settings.ControllWithScreenTouch;
                break;
            default:
                break;
        }
        GameManager.Instance.Options = opt;
        GameManager.Instance.SaveData();
    }
}