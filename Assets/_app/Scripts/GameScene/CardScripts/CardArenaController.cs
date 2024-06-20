using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardArenaController : MonoBehaviour, IInspect
{
    [Header("Inspect")]
    public bool isInspected = false;

    private TurnSystem _ts;

    void Start(){
        _ts = TurnSystem.Instance;
    }

    public void onStartInspect()
    {
        isInspected = true;
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(GetComponent<DisplayCard>().Card);
    }

    public void onStopInspect()
    {
        isInspected = false;
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().HideInfo();
    }

 
}
