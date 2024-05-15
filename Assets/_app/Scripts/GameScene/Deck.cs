using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public CardDatabase db;

    public Hand hand;

    public List<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        var cards = db.cardService.GetCards();
        deck = db.GetComponent<CardDatabase>().toList(cards);
        Shuffle();
    }

    public void Shuffle(){
        deck = deck.OrderBy(_ => Guid.NewGuid()).ToList();
    }

    public void Draw(int count){
        if(count >= 1 && deck.Count >= count){
            for (int i=0; i<count; i++){
                hand.AddCard(deck[0]);
                deck.RemoveAt(0);
            }
            
        }
    }
}
