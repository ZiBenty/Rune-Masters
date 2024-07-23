using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstanceGameManager : MonoBehaviour
{

    void Awake()
    {
        SceneManager.activeSceneChanged += SetupScene;
        
    }

    private void SetupScene(Scene current, Scene next){
        bool set;
        if (next.name == "GameScene")
            set = true;
        else if (next.name == "MainMenu")
            set = false;
        else
            return;
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.SetActive(set);
        if (next.name == "GameScene"){
            gameManager.GetComponent<GameManager>().OnEnable();
            gameManager.GetComponent<TurnSystem>().OnEnable();
        }
        
    }


}
