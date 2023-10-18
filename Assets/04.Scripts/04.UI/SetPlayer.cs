using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayer : MonoBehaviour
{
    private Button button;
    public PlayerStatManager.PlayerType playerType;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Active);
    }

    public void Active()
    {
        PlayerStatManager.Instance.ChangePlayerType(playerType);
    }
}
