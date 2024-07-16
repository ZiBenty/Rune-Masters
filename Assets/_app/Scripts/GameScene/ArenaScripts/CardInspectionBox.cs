using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardInspectionBox : MonoBehaviour
{
    [SerializeField]
    private GameObject CardVisualFull;
    public void ShowInfo(GameObject card){
        HideInfo();
        GameObject copy = Instantiate(card);
        copy.GetComponentInChildren<CardInfo>().LoadInfo(card.GetComponentInChildren<CardInfo>().BaseInfo);
        Destroy(copy.transform.GetChild(1).transform.GetChild(0).gameObject); // removes visual from copy object
        copy.transform.SetParent(transform, false);

        var visual = Instantiate(CardVisualFull);
        visual.transform.SetParent(copy.transform.GetChild(1).transform, false);
        visual.TryGetComponent<CardDisplay>(out var display);
        display.LoadCard();

        //display.transform.localPosition = Vector2.zero;
        display.transform.localScale = new Vector3(7.4f, 7.4f, 7.4f);
        //TODO: remove drag and inspect possibility
        copy.GetComponent<PlayScript>().SetcanDrag(true);
        copy.GetComponent<PlayScript>().SetcanInspect(true);
        copy.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void HideInfo(){
        try{
            if (transform.GetChild(0) != null)
                Destroy(transform.GetChild(0).GameObject());
        }catch{
        }
        
    }

}
