using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllBtn : MonoBehaviour
{
    public Button rightMoveBtn;
    public Button centerMoveBtn;
    public Button leftMoveBtn;

    private void Start()
    {
        var playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rightMoveBtn.onClick.AddListener(() => playerController.MoveWithButton(PlayerController.MoveTo.Right));
        centerMoveBtn.onClick.AddListener(() => playerController.MoveWithButton(PlayerController.MoveTo.Forward));
        leftMoveBtn.onClick.AddListener(() => playerController.MoveWithButton(PlayerController.MoveTo.Left));
    }
}
