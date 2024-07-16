using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    public Card BaseInfo { get; set; } // once setted, it cannot change
    public Card TempInfo { get; set; } // this is allowed to change during gameplay

    public void LoadInfo(Card c){
        BaseInfo = c;
        TempInfo = c;
    }

    public void ResetInfo(){
        TempInfo = BaseInfo;
    }
}
