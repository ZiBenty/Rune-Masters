using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceGameManager : MonoBehaviour
{

    void Awake()
    {
        GameObject touchManger = GameObject.Find("TouchManager");
        touchManger.GetComponent<DragDrop>().enabled = true;
        touchManger.GetComponent<Inspect>().enabled = true;
        touchManger.GetComponent<TouchManager>().enabled = true;
        touchManger.SetActive(true);
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<TargetHandler>().enabled = true;
        gameManager.GetComponent<TurnSystem>().enabled = true;
        gameManager.GetComponent<GameManager>().enabled = true;
        gameManager.SetActive(true);
        gameManager.GetComponent<GameManager>().OnEnable();
        gameManager.GetComponent<TurnSystem>().OnEnable();
    }

    void OnDestroy(){
        GameObject touchManger = GameObject.Find("TouchManager");
        touchManger.GetComponent<DragDrop>().enabled = false;
        touchManger.GetComponent<Inspect>().enabled = false;
        touchManger.GetComponent<TouchManager>().enabled = false;
        touchManger.SetActive(false);
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<TargetHandler>().enabled = false;
        gameManager.GetComponent<TurnSystem>().enabled = false;
        gameManager.GetComponent<GameManager>().enabled = false;
        gameManager.SetActive(false);
    }

}
