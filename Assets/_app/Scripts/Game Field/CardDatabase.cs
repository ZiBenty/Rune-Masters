using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<Card> Cards {get; set;}

    void Start(){
        Cards = new List<Card>();
    }

}
