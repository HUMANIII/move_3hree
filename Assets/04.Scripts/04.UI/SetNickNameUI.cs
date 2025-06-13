using TMPro;
using UnityEngine;

public class SetNickNameUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public void SetNickName()
    {
        GameManager.Instance.SetNickName(inputField.text);
    }
}
