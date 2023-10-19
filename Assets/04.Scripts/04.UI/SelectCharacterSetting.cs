using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterSetting : MonoBehaviour
{
    [SerializeField] private GameObject jailbreak;
    [SerializeField] private GameObject greenApple;
    [SerializeField] private GameObject banana;
    [SerializeField] private GameObject mithrill;

    private void Start()
    {
        UpdatePhoneUnlockInfo();
    }

    public void UpdatePhoneUnlockInfo()
    {
        var psm = PlayerStatManager.Instance;

        jailbreak.SetActive((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.JailbreakedPhone) != 0);
        greenApple.SetActive((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.GreenApplePhone) != 0);
        banana.SetActive((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.BananaPhone) != 0);
        mithrill.SetActive((psm.upgrade.phoneUnlockInfo & PlayerStatManager.PhoneUnlockInfo.MithrillPhone) != 0);
    }
}
