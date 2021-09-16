using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    private void Awake()
    {
        PanelsConfiguration();
    }
    [SerializeField] private List<UIPanel> panels = new List<UIPanel>();
    [SerializeField] private UIPanel currentActive = null;
    private void PanelsConfiguration()
    {
        // Do Not use Flags because it will make collision of enum has more then 1 enum;

        /// <summary>
        /// Since Findobjectoftype doesn't find inactive objects, you need to use Resources.FindObjectsOfTypeAll. 
        /// But it gives you including assets,prefabs, textures and etc,
        /// So you need to filter it by: 
        /// IsPersistent - Determines if an object is stored on disk.
        /// HideFlags.NotEditable - The object will not be editable in the inspector.
        /// HideFlags.HideAndDontSave - The GameObject is not shown in the Hierarchy, not saved to to Scenes, and not unloaded
        /// by Resources.UnloadUnusedAssets.
        /// This is most commonly used for GameObjects which are created by a script and are purely under the script's control.
        /// </summary>
        /// <param name="UIPanel">UIPanel is the script that is attached to the gameobject</param>
        /// <returns></returns>
        foreach (UIPanel panel in Resources.FindObjectsOfTypeAll(typeof(UIPanel)) as UIPanel[])
        {
#if UNITY_EDITOR
            if (EditorUtility.IsPersistent(panel.gameObject.transform.root.gameObject)) return;
#endif
            if (!(panel.gameObject.hideFlags == HideFlags.NotEditable ||
            panel.gameObject.hideFlags == HideFlags.HideAndDontSave))
                panels.Add(panel);
            if (panel.gameObject.activeSelf)
                //geting the active UIPanel
                currentActive = panel;
        }
    }
    public void ChangePanelTo(UIPanel panelToChange)
    {
        PanelChanger(panelToChange.panelType);
    }

    public void PanelChanger(PanelType panelToChange)
    {
        // to do if playing change to PlayerMenu
        if (panels.Count <= 1) { Debug.Log("List Is empty"); PanelsConfiguration(); }
        foreach (UIPanel panel in panels)
        {
            // if (additionalValues.buttonClip != null)
            //     gameManager.PlayAudio(Managers.AudioType.SFXVolume, additionalValues.buttonClip);
            if (panel.panelType == panelToChange && currentActive.panelType != panel.panelType)
            {
                if (currentActive != null)
                    currentActive.gameObject.SetActive(false);
                currentActive = panel;
                panel.gameObject.SetActive(true);
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

}
