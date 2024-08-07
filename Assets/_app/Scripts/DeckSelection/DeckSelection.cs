using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckSelection : MonoBehaviour
{
    public static DeckSelection Instance {get; private set;}

    public string PlayerDeckChoice = "";
    public string EnemyDeckChoice = "";

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }
}
