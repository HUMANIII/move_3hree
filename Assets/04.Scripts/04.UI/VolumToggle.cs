using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumToggle : MonoBehaviour
{
    [SerializeField] private string Name;

    public void SetToggle()
    {
        SoundManager.Instance.ToggleMute(Name);
    }
}
