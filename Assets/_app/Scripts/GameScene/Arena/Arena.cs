using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

using static Constants;

public class Arena : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Column0, Column1, Column2, Column3, Column4;

    public List<List<GameObject>> Columns;

    public List<ArenaLine> Lines;

    public void Awake(){
        Columns = new List<List<GameObject>>
        {
            Column0,
            Column1,
            Column2,
            Column3,
            Column4
        };
    }

    public void DeColorSlots(){
        foreach(ArenaLine line in Lines){
            for(int i = 0; i < line.transform.childCount; i++){
                line.transform.GetChild(i).GetChild(0).GetComponent<ArenaCardSlot>().OutlineComp.enabled = false;
            }
        }
    }

    public void SetSlotsInspectable(bool b){
        foreach(ArenaLine line in Lines){
            for(int i = 0; i < line.transform.childCount; i++){
                line.transform.GetChild(i).GetChild(0).GetComponent<ArenaCardSlot>().SetcanInspect(b);
                line.transform.GetChild(i).GetChild(0).GetComponent<BoxCollider2D>().enabled = b;
            }
        }
    }
    
}
