using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(i).GetComponent<RectTransform>());
    }

    public bool IsSlotInLine(GameObject slot){
        for(int i = 0; i< transform.childCount; i++){
            if(ReferenceEquals(transform.GetChild(i).gameObject, slot)){
                return true;
            }
        }
        return false;
    }

}
