using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardInspectionBox : MonoBehaviour
{
    [SerializeField]
    private GameObject CardVisualFull;
    public void ShowInfo(Card card){
        HideInfo();
        var visual = Instantiate(CardVisualFull, Vector3.zero, Quaternion.identity);
        visual.transform.SetParent(transform);
        visual.TryGetComponent<DisplayCard>(out var display);
        display.LoadCard(card);
        display.transform.localPosition = transform.position;
        display.transform.localScale = new Vector3(7.8f, 7.8f, 7.8f);
    }

    public void HideInfo(){
        try{
            if (transform.GetChild(0) != null)
                Destroy(transform.GetChild(0).GameObject());
        }catch{
        }
        
    }

}
