using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    public Text action, keyButton;
    private string bindingPath; // the index of the binding effective path

    [SerializeField] private InputActionReference movement;
    [SerializeField] private Button button;
    private GameplayInput gamePlayInput;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;


    public void StartRebinding()
    {
        movement.action.Disable();
        button.interactable = false;
        this.keyButton.text = "Waiting for input";
        rebindingOperation = movement.action.PerformInteractiveRebinding().WithControlsExcluding("Mouse").
        OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start();

    }

    private void RebindComplete()
    {
        bindingPath =
        this.keyButton.text = InputControlPath.ToHumanReadableString(bindingPath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        button.interactable = true;
        DebugKeyBindings();
        rebindingOperation.Dispose();
        if (rebindingOperation.completed)
            movement.action.Enable();
    }
    public void UpdateText(string action, string keyButton, string path)
    {
        this.action.text = action;
        this.keyButton.text = keyButton;
        this.bindingPath = path;
    }
    private void DebugKeyBindings()
    {
        foreach (InputBinding binding in movement.action.bindings)
        {
            if (binding.isComposite) continue; // checks if it's not a binding but a composite type like 2DVector

            //Getting the binding name to change the text later
            string str = InputControlPath.ToHumanReadableString(binding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            Debug.Log(str);
        }
    }
}


// to do change binding for a specific playerinput asset