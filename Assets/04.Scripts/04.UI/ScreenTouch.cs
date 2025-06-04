using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenTouch : MonoBehaviour, IPointerDownHandler
{
    public PlayerController.MoveTo moveTo;
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().MoveWithButton(moveTo);        
    }
}
