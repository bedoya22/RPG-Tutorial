using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineGameManager : MonoBehaviour
{
    #region Singleton
    private static OfflineGameManager _instance;
    public static OfflineGameManager Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _instance = this;
    }
    #endregion
    #region Cursor Hidder
    public void CursorMode(bool isActive, CursorLockMode typeCursor)
    {
        Cursor.visible = isActive;
        Cursor.lockState = typeCursor;
    }
    #endregion
}
