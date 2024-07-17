using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static CardDatabase Instance;
    public CardService cardService {get; set;}

    void Awake(){
        Instance = this;
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
