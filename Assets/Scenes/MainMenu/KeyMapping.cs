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

        // getting what type of hardware does he use
        Debug.Log(playerInput.currentControlScheme);
        GameplayInput gamePlayInput = new GameplayInput();
        foreach (InputBinding binding in gamePlayInput.Player.Movement.bindings)
        {
            //Debug.Log(binding.action); // says the action name
            string str = binding.path.Substring(binding.path.LastIndexOf('/') + 1); // checks if it's a keybinding and not another property
            // gets the binding name
            if (binding.name.Contains("Movemen")) continue;
            // Debug.Log(binding.path.Substring(binding.path.LastIndexOf('/') + 1)); // says the binding name
            Instantiate<Key>(keyMappingPreFab, Vector3.zero, Quaternion.identity, this.transform).onCreation(binding.name, str);
        }
        // Debug.Log(playerInput.currentActionMap.ToString());
        // playerInput.SwitchCurrentActionMap("UI"); // change action map in the defaultinput system from Player to UI
        // Debug.Log(playerInput.currentActionMap.ToString());

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void TypeController(InputAction.CallbackContext context)
    {
        string name = context.valueType.FullName;
        Debug.Log(name);
    }
}
