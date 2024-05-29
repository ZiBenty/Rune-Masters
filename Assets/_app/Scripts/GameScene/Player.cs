using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Deck;
    private Deck _deck;
    public GameObject Hand;
    private Hand _hand;
    public GameObject SummonLine;
    private int Life {get; set;}

    void Start()
    {
        _deck = Deck.GetComponent<Deck>();
        _hand = Hand.GetComponent<Hand>();
    }

    void Update()
    {
        
    }

    public IEnumerator Draw(int count){
        if(count >= 1 && _deck.DeckList.Count >= count){
            for (int i=0; i<count; i++){
                _hand.AddCard(_deck.DeckList[0]);
                _deck.DeckList.RemoveAt(0);
                yield return new WaitForSeconds(0.3f);
            }     
        }
    }
    
}
