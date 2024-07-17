using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TouchManager : MonoBehaviour
{
    public TouchControl LastTouch;

    private void Awake(){
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
