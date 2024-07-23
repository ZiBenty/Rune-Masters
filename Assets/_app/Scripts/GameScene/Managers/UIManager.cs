using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}
    [Header("Hints")]
    [SerializeField] private GameObject HintBox;
    [SerializeField] private GameObject WarningBox;
    [SerializeField] private GameObject ConfirmYes, ConfirmNo;

    [Header("Victory")]
    [SerializeField] private GameObject VictoryPanel;

    [Header("Turns")]
    [SerializeField] private TMP_Text TurnText;
    [SerializeField] private GameObject MovePhase, EndTurn;

    void Awake(){
        if (Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
        ConfirmYes.GetComponent<Button>().onClick.AddListener(TargetHandler.Instance.OnConfirmYes);
        ConfirmNo.GetComponent<Button>().onClick.AddListener(TargetHandler.Instance.OnConfirmNo);
        MovePhase.GetComponent<Button>().onClick.AddListener(TurnSystem.Instance.movePhase);
        EndTurn.GetComponent<Button>().onClick.AddListener(TurnSystem.Instance.onEndTurn);
    }

    public void SetActiveConfirmButtons(bool b){
        ConfirmYes.SetActive(b);
        ConfirmNo.SetActive(b);
    }

    public void ChangeHintBox(bool enabled, string text = ""){
        HintBox.GetComponent<Image>().enabled = enabled;
        HintBox.transform.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void ChangeWarningBox(bool enabled, string text = ""){
        WarningBox.GetComponent<Image>().enabled = enabled;
        WarningBox.transform.GetComponentInChildren<TMP_Text>().text = text;
    }

    public void VictoryPanelActivate(string text = ""){
        VictoryPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = text;
        VictoryPanel.SetActive(true);
    }

    public void UpdateTurnText(string text = ""){
        TurnText.text = text;
    }

    public IEnumerator HintForSeconds(string text = "", float seconds = 0){
        ChangeWarningBox(true, text);
        yield return new WaitForSeconds(seconds);
        ChangeWarningBox(false);
    }
}
