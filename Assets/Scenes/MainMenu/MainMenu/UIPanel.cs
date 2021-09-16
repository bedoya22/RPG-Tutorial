using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PanelType
{
    None, MainMenu, GameOver, Settings, Serverlist
}
public class UIPanel : MonoBehaviour
{

    public PanelType panelType;
}
