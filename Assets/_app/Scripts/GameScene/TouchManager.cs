using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction showInfoAction;
    private InputAction pressTouchedAction;
    public Action<int, Vector2> Held;
    private HashSet<int> heldInvoked;

    public GameObject Card;
    public GameObject CardInspectionBox;

    private void Awake(){
        playerInput = GetComponent<PlayerInput>();
        showInfoAction = playerInput.actions["Show Info"];
        pressTouchedAction = playerInput.actions["TouchPress"];
    }

    //metodi per iscriversi all'evento ed ascoltarlo
    private void OnEnable(){
        heldInvoked = new();
        showInfoAction.performed += InfoShow;
        pressTouchedAction.performed += PressTouched;
    }

    private void OnDisable(){
        showInfoAction.performed -= InfoShow;
        pressTouchedAction.performed -= PressTouched;
    }

    private void PressTouched(InputAction.CallbackContext context){
        //float value = context.ReadValue<float>();
        //Debug.Log(value);
    }

    private void InfoShow(InputAction.CallbackContext context){
    }
}
