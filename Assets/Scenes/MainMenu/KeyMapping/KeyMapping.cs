using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeyMapping : MonoBehaviour
{
    public Key keyMappingPreFab;
    //public InputAction input; // to add a custome made input action
    [SerializeField] UnityEngine.InputSystem.PlayerInput playerInput;
    void Start()
    {
        // getting what type of hardware does he use Keyboard or gamepad
        Debug.Log(playerInput.currentControlScheme);
        int bindingsIndex = 1; // skipping the binding compossite
        //Creating the new GameplayInput class as in the asset inputsystem
        GameplayInput gamePlayInput = new GameplayInput();
        // Iteration through each binding. this allows also you not only get the specific binding but also passing it's path to change later
        foreach (InputBinding binding in gamePlayInput.Player.Movement.bindings)
        {
            if (binding.isComposite) continue; // checks if it's not a binding but a composite type like 2DVector

            //Getting the binding name to change the text later
            string str = InputControlPath.ToHumanReadableString(binding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
            Debug.Log(str);
            //binding.name is the binding name that is written in the gameplayinput.cs generated file
            Instantiate<Key>(keyMappingPreFab, Vector3.zero, Quaternion.identity, this.transform).UpdateText(binding.name, str, bindingsIndex);
            bindingsIndex++;
        }

    }

}


// Debug.Log(binding.path.Substring(binding.path.LastIndexOf('/') + 1)); // says the binding name
// Debug.Log(playerInput.currentActionMap.ToString());
// playerInput.SwitchCurrentActionMap("UI"); // change action map in the defaultinput system from Player to UI
// Debug.Log(playerInput.currentActionMap.ToString());