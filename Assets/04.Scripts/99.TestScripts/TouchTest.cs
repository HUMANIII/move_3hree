using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        var message = string.Empty;

        foreach(var touch in Input.touches) 
        {
            message += "Touch ID : " + touch.fingerId + "\tPhase" + touch.phase + "\tPosition" + touch.position + "\tDeltaPposition" + touch.deltaPosition + "\tDeltaTime" + touch.deltaTime;
        }

        text.text = message;
    }
}
