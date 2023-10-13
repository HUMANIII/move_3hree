using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    private void Start()
    {
        btn.onClick.AddListener(PlayerStatManager.Instance.TestMethod);
    }

}


