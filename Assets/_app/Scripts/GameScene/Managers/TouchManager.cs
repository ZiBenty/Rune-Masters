using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance {get; private set;}
    public TouchControl LastTouch;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
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

    public void onBackButton(){
        SceneManager.LoadScene("MainMenu");
    }

}
