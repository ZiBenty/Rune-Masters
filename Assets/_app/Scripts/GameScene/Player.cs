using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Deck;
    public GameObject Hand;
    public GameObject SummonLine;
    private int Life {get; set;}

    public void Draw(int count){
        var deck = Deck.GetComponent<Deck>();
        var hand = Hand.GetComponent<Hand>();
        if(count >= 1 && deck.DeckList.Count >= count){
            for (int i=0; i<count; i++){
                hand.AddCard(deck.DeckList[0]);
                deck.DeckList.RemoveAt(0);
            }     
        }
    }
    
}
