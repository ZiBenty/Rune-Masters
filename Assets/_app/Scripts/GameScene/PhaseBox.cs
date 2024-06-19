using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseBox : MonoBehaviour
{
    [SerializeField]
    private TurnSystem ts;

    private string PhaseName;
    private bool isPhase;

    void Start(){
        PhaseName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        switch(PhaseName){
            case "DrawPhase":
                isPhase = ts.isDrawPhase;
                break;
            case "MovePhase":
                isPhase = ts.isMovePhase;
                break;
            case "CombatPhase":
                isPhase = ts.isCombatPhase;
                break;
            case "EndPhase":
                isPhase = ts.isEndPhase;
                break;
        }
        
        if(isPhase)
            GetComponent<Image>().color = Color.green;
        else
            GetComponent<Image>().color = Color.white;
    }
}
