using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    public GameObject HintBox;
    public GameObject WarningBox;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    public void ChangeHintBox(bool enabled, string text = ""){
        HintBox.GetComponent<Image>().enabled = enabled;
        HintBox.transform.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void ChangeWarningBox(bool enabled, string text = ""){
        WarningBox.GetComponent<Image>().enabled = enabled;
        WarningBox.transform.GetComponentInChildren<TMP_Text>().text = text;
    }

    public IEnumerator HintForSeconds(string text = "", float seconds = 0){
        ChangeWarningBox(true, text);
        yield return new WaitForSeconds(seconds);
        ChangeWarningBox(false);
    }
}
