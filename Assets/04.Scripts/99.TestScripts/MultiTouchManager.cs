using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiTouchManager : MonoBehaviour
{
    public bool IsTouching {  get; private set; }

    public float minZoomInch = 0.2f;
    public float maxZoomInch = 0.5f;

    public float minZoomPixel;
    public float maxZoomPixel;
    public float ZoomInch {  get; private set; }

    private List<int> fingerIDList = new();

    private void Awake()
    {
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);

        Debug.Log(Screen.dpi);
    }

    
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        
        foreach ( var touch in Input.touches )
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerIDList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    fingerIDList.Remove(touch.fingerId);
                    break;
            }
        }
#endif

#if UNITY_EDITOR || UNITY_STANDALONE

#elif UNITY_ANDROID || UNITY_IOS
        UpdatePinchToZoom();
#endif
    }

    private void UpdatePinchToZoom()
    {
        if (fingerIDList.Count >= 2)
        {
            Vector2[] prevTouchPos = new Vector2[2];
            Vector2[] curTouchPos = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                var touch = Array.Find(Input.touches, x => x.fingerId == fingerIDList[i]);

                curTouchPos[i] = touch.position;
                prevTouchPos[i] = curTouchPos[i] - touch.deltaPosition;
            }

            var prevDist = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);
            var curDist = Vector2.Distance(curTouchPos[0], curTouchPos[1]);
            var distPixel = curDist - prevDist;
            //var distInch = distPixel / Screen.dpi;
            Debug.Log(distPixel);
        }
    }
}
