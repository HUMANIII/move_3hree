using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private string Name;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetVolum()
    {
        SoundManager.Instance.SetSound(Name,slider.value);
    }
}
