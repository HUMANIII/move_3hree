using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Flags]
    public enum SoundState
    {
        None = 0,
        MuteMaster = 1 << 0,
        MuteBGM = 1 << 1,
        MuteSFX = 1 << 2,
    }

    public static SoundManager Instance = null;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource SFX;

    [SerializeField] private List<AudioClip> Move;
    [SerializeField] private AudioClip MoveSpecific;
    [SerializeField] private AudioClip Select;
    [SerializeField] private AudioClip Die;

    [SerializeField] private List<AudioClip> StageBGM;
    [SerializeField] private List<AudioClip> StageBGMJailbreak;
    [SerializeField] private AudioClip MainMenuBGM;

    public SoundState soundState;
    public float masterVolume;
    public float BGMVolume;
    public float SFXVolume;

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

    public void SetSound(string name, float value)
    {
        audioMixer.SetFloat(name, value);
    }

    public void ToggleMute(string name)
    {
        switch(name)
        {
            case "Master":
                soundState ^= SoundState.MuteMaster;
                break;
            case "BGM":
                soundState ^= SoundState.MuteBGM;
                break;
            case "SFX":
                soundState ^= SoundState.MuteSFX;
                break;
            default: break;
        }

        BGM.mute = (soundState & SoundState.MuteBGM) != 0;
        SFX.mute = (soundState & SoundState.MuteSFX) != 0;
        if((soundState & SoundState.MuteMaster) != 0)
        {
            BGM.mute = true;
            SFX.mute = true;
        }        
    }
}
