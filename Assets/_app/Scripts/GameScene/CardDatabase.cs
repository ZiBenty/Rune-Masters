using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public CardService cardService {get; set;}

    void Start(){
        cardService = new CardService();
    }

    [Tooltip("Transforms a IEnumerable<Card> list into List<Card>")]
    public List<Card> toList(IEnumerable<Card> list)
    {
        List<Card> l = new List<Card>();
        foreach (Card card in list)
        {
            l.Add(card);
        }
        return l;
    }

}
