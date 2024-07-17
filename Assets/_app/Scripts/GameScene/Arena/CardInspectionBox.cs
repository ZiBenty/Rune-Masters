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
        copy.GetComponent<CardInfo>().LoadInfo(card.GetComponent<CardInfo>().BaseInfo);
        if (copy.transform.childCount != 0)
            Destroy(copy.transform.GetChild(0).gameObject); // removes visual from copy object
        copy.transform.SetParent(transform, false);

        var visual = Instantiate(CardVisualFull);
        visual.transform.SetParent(copy.transform, false);
        visual.TryGetComponent<CardDisplay>(out var display);
        display.LoadCard();

        //display.transform.localPosition = Vector2.zero;
        display.transform.localScale = new Vector3(7.4f, 7.4f, 7.4f);
        copy.GetComponent<CardState>().Location = Constants.Location.Inspected;
    }

    public void HideInfo(){
        try{
            if (transform.GetChild(0) != null)
                Destroy(transform.GetChild(0).GameObject());
        }catch{
        }
        
    }

}
