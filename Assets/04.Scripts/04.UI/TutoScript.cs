using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutoScript : MonoBehaviour
{
    [SerializeField] private string[] texts;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private GameObject panel;

    private int counter;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        SetTutos();
    }
    public void SetTutos()
    {
        Debug.Log("settt");
        if(counter == images.Length) 
        {
            counter = 0;
            panel.SetActive(false);
            return;
        }
        Debug.Log("set");
        text.text = texts[counter];
        image.sprite = images[counter];

        counter++;
    }
}
