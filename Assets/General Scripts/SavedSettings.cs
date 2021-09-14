using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SavedSettings/SettingsSO")]
public class SavedSettings : ScriptableObject
{
    [Header("OptionMenu")]
    public Color normal;
    public Color selected;
    public float musicVolume;
    public float sfxVolume;

    [Header("SceneManagement")]
    public int buildIndex;
    public int timeToWait;
    public Sprite[] image;

    public int imageIndex;
}
