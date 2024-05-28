using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour, IInspect
{
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
