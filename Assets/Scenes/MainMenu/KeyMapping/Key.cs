using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Text action, keyButton;

    public void onCreation(string action, string keyButton)
    {
        this.action.text = action;
        this.keyButton.text = keyButton;
    }
}
