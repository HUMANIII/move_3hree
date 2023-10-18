using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SFX;

    [SerializeField] private List<AudioClip> Move;
    [SerializeField] private AudioClip MoveSpecific;
    [SerializeField] private AudioClip Select;
    [SerializeField] private AudioClip Die;

    [SerializeField] private List<AudioClip> StageBGM;
    [SerializeField] private List<AudioClip> StageBGMJailbreak;
    [SerializeField] private AudioClip MainMenuBGM;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        MoveToMainMenu();
        BGM.loop = true;
    }
        
    public void MoveToMainMenu()
    {
        BGM.Stop();
        BGM.clip = MainMenuBGM;
        BGM.Play();
    }

    public void PlayStageBGM(int stage)
    {
        BGM.Stop();
        if (PlayerStatManager.Instance.playerType == PlayerStatManager.PlayerType.JailbreakedPhone)
        {
            BGM.clip = StageBGMJailbreak[stage - 1];
        }
        else
        {
            BGM.clip = StageBGM[stage - 1];
        }
        BGM.Play();
    }

    public void MoveSound(bool isSpecific = false)
    {
        var moveSound = PlayerStatManager.Instance.playerType switch
        {
            PlayerStatManager.PlayerType.DefaultPhone => Move[0],
            PlayerStatManager.PlayerType.JailbreakedPhone => Move[1],
            PlayerStatManager.PlayerType.GreenApplePhone => Move[2],
            PlayerStatManager.PlayerType.BananaPhone => Move[3],
            PlayerStatManager.PlayerType.MithrillPhone when isSpecific == false => Move[4],
            PlayerStatManager.PlayerType.MithrillPhone when isSpecific == true => MoveSpecific,
            _ => Move[0]
        };
        SFX.PlayOneShot(moveSound);
    }

    public void GameOver()
    {
        SFX.PlayOneShot(Die);
    }

    public void ClickSound()
    {
        SFX.PlayOneShot(Select);
    }
}
