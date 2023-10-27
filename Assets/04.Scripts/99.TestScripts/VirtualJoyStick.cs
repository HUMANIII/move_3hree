using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image stick;
    public float radius;
    private Vector3 originalPos;
    private RectTransform rectTr;
    private Vector2 value;

    private int pointerId;
    private bool IsDragging = false;

    public enum Axis
    {
        Horizontal,
        Vertical,
    }
    private void Start()
    {
        originalPos = stick.rectTransform.position;
        rectTr = GetComponent<RectTransform>();
    }

    public float GetAxis(Axis axis)
    {
        return axis switch
        {
            Axis.Horizontal => value.x,
            Axis.Vertical => value.y,
            _ => 0f,
        };
    }

    private void Update()
    {
        //Debug.Log($"{GetAxis(Axis.Horizontal)}, {GetAxis(Axis.Vertical)}");
    }

    private void UpdatePointerPos(Vector3 eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTr, eventData, null, out var eventWorldPoint);
        var deltaPos = Vector3.ClampMagnitude(eventWorldPoint - originalPos, radius);
        stick.transform.position = deltaPos + originalPos;
        value = deltaPos.normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;
        UpdatePointerPos(eventData.position);
    }    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsDragging)
            return;
        IsDragging = true;
        pointerId = eventData.pointerId;
        UpdatePointerPos(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        IsDragging = false;
        stick.transform.position = originalPos;
        value = Vector2.zero;
    }
}
