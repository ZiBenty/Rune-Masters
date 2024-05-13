using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardDatabase db;

    public Hand hand;

    public List<Card> deck;

    // Start is called before the first frame update
    void Start()
    {
        var cards = db.cardService.GetCards();
        deck = db.GetComponent<CardDatabase>().toList(cards);
    }

    public void Shuffle(){
        
    }

    public void Draw(int count){
        if(count >= 1){
            for (int i=0; i<count; i++){
                hand.AddCard(deck[0]);
                deck.RemoveAt(0);
            }
            
        }
    }
}
