using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour, IInspect
{
    [Header("Inspect")]
    private bool _canInspect = true;

    public void SetcanInspect(bool b){
        _canInspect = b;
    }
    public bool GetcanInspect(){
        return _canInspect;
    }
    public void onStartInspect()
    {
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().HideInfo();
    }

    public void onStopInspect()
    {
        //throw new System.NotImplementedException();
    }
}
