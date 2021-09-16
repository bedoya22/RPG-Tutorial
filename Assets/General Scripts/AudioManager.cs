using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Flags]
public enum AudioType { MusicVolume, SFXVolume }


public struct AudioChangeStruct
{
    public float vol;
    public AudioType Audiotype;

}
public class AudioManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource sFXSource;
    [SerializeField] private AudioMixer audiomixer;
    private static AudioManager _instance;


    public static AudioManager Instance { get { return _instance; } }
    /// When ingame the characters speaking or events that rquries to change volume in runtime
    public delegate void OnAudioChange(AudioChangeStruct change);

    /// <summary>
    /// When AudioChanger is invoked it will change values for the mixers
    /// </summary>
    public static OnAudioChange AudioChanger;
    private void Awake()
    {
        _instance = this; // this will make sure no null references
        AudioChanger += AudioChangeAction;
    }
    private void OnDisable()
    {
        AudioChanger -= AudioChangeAction;
    }



    //Changing values per AudioSourceGroups
    private void AudioChangeAction(AudioChangeStruct changeValues)
    {
        audiomixer.SetFloat(changeValues.Audiotype.ToString(), Mathf.Log10(changeValues.vol) * 20);
        PlayerPrefs.SetFloat(changeValues.Audiotype.ToString(), Mathf.Log10(changeValues.vol) * 20);
    }



    #region AudioSettings
    /// <summary>
    /// Changing thr audio through the main Manager. Can happen only in Gamestat.MainMenu
    /// </summary>
    /// <param name="type">AudioType and type if it's music or SFX</param>
    /// <param name="volume">Volume from 0 to 1</param>
    public void SetAudioVolume(AudioType type, float volume)
    {
        AudioChanger.Invoke(new AudioChangeStruct { Audiotype = type, vol = volume });
    }
    /// <summary>
    /// Getting the volume for each slider
    /// </summary>
    /// <param name="type">If it's music or SFX</param>
    /// <returns></returns> 
    public float GetAudio(AudioType type)
    {
        return Mathf.Pow(10, PlayerPrefs.GetFloat(type.ToString(), 0) / 20);
    }
    public void PlayAudio(AudioType type, AudioClip audioClip)
    {
        if (audioClip == null) return;
        switch (type)
        {
            case AudioType.MusicVolume:
                musicSource.PlayWithClip(audioClip);
                break;
            case AudioType.SFXVolume:
                sFXSource.PlayWithClip(audioClip);
                break;
        }

    }
}
#endregion
public static class AudioSourceExtension
{
    public static AudioSource PlayWithClip(this AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        return audioSource;


        //in order to chain static and non static you need to return the class.
    }
}
