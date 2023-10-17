using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        MoveToMainMenu();
        audioSource.loop = true;
    }
        
    public void MoveToMainMenu()
    {
        audioSource.Stop();
        audioSource.clip = MainMenuBGM;
        audioSource.Play();
    }

    public void PlayStageBGM(int stage)
    {
        audioSource.Stop();
        if (PlayerStatManager.Instance.playerType == PlayerStatManager.PlayerType.JailbreakedPhone)
        {
            audioSource.clip = StageBGMJailbreak[stage - 1];
        }
        else
        {
            audioSource.clip = StageBGM[stage - 1];
        }
        audioSource.Play();
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
        audioSource.PlayOneShot(moveSound);
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(Die);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(Select);
    }
}
