using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectOption : MonoBehaviour
{
    
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private ContentSizeFitter csf;
    [SerializeField] private LayoutGroup lg;
    [SerializeField] private RectTransform viewport;

    private bool isSetted = false;


    private void SetData()    
    {
        //만약 느리면 인스펙터에서 할당해주기
        if (isSetted)
            return;

        isSetted = true;
        scrollRect ??= GetComponent<ScrollRect>();
        content ??= scrollRect.content;
        viewport ??= scrollRect.viewport;
        csf ??= scrollRect.GetComponentInChildren<ContentSizeFitter>();
        lg ??= content.GetComponent<LayoutGroup>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

    }

    public IEnumerator SetLayoutGroupOption()
    {
        SetData();
        if (lg != null)
        {
            lg.enabled = true;
        }
        if (csf != null)
        {
            csf.enabled = true;
        }
        //왜인지 자세히는 모르겠지만 이렇게 하면 잘됐음 ㅎㅎ
        yield return null;
        yield return null;
        
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);

        if (lg != null)
        {
            lg.enabled = false;
        }
        if (csf != null)
        {
            csf.enabled = false;
        }
    }

    private void OnScrollValueChanged(Vector2 position)
    {
        CheckContentVisibility();
    }

    private void CheckContentVisibility()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            RectTransform child = content.GetChild(i) as RectTransform;
            if (child == null)
                continue;

            child.gameObject.SetActive(IsRectTransformInsideViewport(child));
        }
    }

    private bool IsRectTransformInsideViewport(RectTransform rectTransform)
    {
        Vector3[] viewportCorners = new Vector3[4];
        Vector3[] rectTransformCorners = new Vector3[4];

        viewport.GetWorldCorners(viewportCorners);
        rectTransform.GetWorldCorners(rectTransformCorners);

        Rect viewportRect = new Rect(viewportCorners[0], viewportCorners[2] - viewportCorners[0]);

        foreach (Vector3 corner in rectTransformCorners)
        {
            if (viewportRect.Contains(corner))
            {
                return true;
            }
        }

        return false;
    }
}
