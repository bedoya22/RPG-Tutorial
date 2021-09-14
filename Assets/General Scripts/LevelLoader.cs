using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int firstTimeOpenApplication = 1;
    const string FirstTime = "FIRST_TIME_OPEN_APPLICATION";
    void Start()
    {
        // Never being init
        if (PlayerPrefs.HasKey(FirstTime))
        {
            PlayerPrefs.SetInt(FirstTime, 1);
        }

        //Checking if it's splash scene or loading scene
        if (PlayerPrefs.GetInt(FirstTime, 1) == 1)
        {
            PlayerPrefs.SetInt(FirstTime, 0);
            //Firsttime open the game
            // to do first image to load
        }
        else
        {

        }
    }

    private void FirstTimeApp()
    {
        // to do setting image and etc
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(FirstTime, 1);
    }
}
