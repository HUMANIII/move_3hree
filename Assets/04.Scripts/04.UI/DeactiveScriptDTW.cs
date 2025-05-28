using DG.Tweening;
using QuantumTek.QuantumUI;
using UnityEngine;

public class DeactiveScriptDTW : MonoBehaviour
{
    private QUI_Element element;

    private void Awake()
    {
        element = transform.parent.GetComponent<QUI_Element>();
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f);
    }

    public void DeactiveGo()
    {
        transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => {
            if (element)
                element.SetActive(false);
            else
                gameObject.SetActive(false);
        });

    }
}
