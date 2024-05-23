using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TouchManager : MonoBehaviour
{
    private Camera MainCamera;
    public TouchControl LastTouch;

    private void Awake(){
        MainCamera = Camera.main;
    }

    public RaycastHit getHitCollider()
    {
        Ray ray = MainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Touchscreen.current.primaryTouch.IsPressed())
                LastTouch = Touchscreen.current.primaryTouch;
        }catch{}
        
    }

}
