using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    const string FirstTime = "FIRST_TIME_OPEN_APPLICATION";
    [SerializeField] private Slider loadingSlider;
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
            StartCoroutine(LoadAsyncRequestScene(SceneManager.GetActiveScene().buildIndex + 1, 2f));
            // to do first image to load
        }
        else
        {

        }
    }


    IEnumerator LoadAsyncRequestScene(int buildIndex, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        AsyncOperation LoadingScene = SceneManager.LoadSceneAsync(buildIndex);
        while (!LoadingScene.isDone)
        {
            loadingSlider.value = Mathf.Clamp01(LoadingScene.progress / 0.9f);
            OfflineGameManager.Instance.CursorMode(true, CursorLockMode.None);
            yield return null;
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
