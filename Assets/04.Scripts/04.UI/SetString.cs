using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetString : MonoBehaviour
{
    public TextMeshProUGUI target;
    public string text;

    public void Active()
    {
        target.text = text;
    }
}
