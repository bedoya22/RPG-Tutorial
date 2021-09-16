using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    public Text action, keyButton;
    private int bindingIndex; // the index of the binding effective path to change for a specific key

    [SerializeField] private InputActionReference movement;
    [SerializeField] private Button button;
    private GameplayInput gamePlayInput;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;


    public void StartRebinding()
    {
        if (movement.action.enabled == false)
        {
            return;
        }
        movement.action.Disable();
        button.interactable = false;
        this.keyButton.text = "Waiting for input";
        rebindingOperation = movement.action.PerformInteractiveRebinding(bindingIndex).WithControlsExcluding("Mouse").
        OnMatchWaitForAnother(0.1f).OnComplete(operation => RebindComplete()).Start();

    }

    private void RebindComplete()
    {
        if (CheckForDuplicates())
        {
            movement.action.RemoveBindingOverride(bindingIndex); // removing if duplicates
        }
        this.keyButton.text = InputControlPath.ToHumanReadableString(movement.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        button.interactable = true;
        rebindingOperation.Dispose();
        if (rebindingOperation.completed)
        {
            string rebinds = movement.action.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("Rebinds", rebinds);
            movement.action.Enable();
        }
    }
    public void UpdateText(string action, string keyButton, int index)
    {
        this.action.text = action;
        this.keyButton.text = keyButton;
        this.bindingIndex = index;
    }
    private bool CheckForDuplicates()
    {
        InputBinding bindingToCheck = movement.action.bindings[bindingIndex];
        foreach (InputBinding binding in movement.action.bindings)
        {
            if (movement.action.GetBindingIndex(binding) == bindingIndex)
                continue;

            if (bindingToCheck.effectivePath == binding.effectivePath)
                return true;
        }
        return false;
    }

    private void DebugBindings()
    {
        for (int binding = 0; binding < movement.action.bindings.Count; binding++)
        {
            if (movement.action.bindings[binding].isComposite) continue; // checks if it's not a binding but a composite type like 2DVector

            //Getting the binding name to change the text later
            string str = InputControlPath.ToHumanReadableString(movement.action.bindings[binding].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            Debug.Log(str);
        }
    }
}


// Debug.Log(rebindingOperation.selectedControl.device); // getting the deviece name
// to do change binding for a specific playerinput asset