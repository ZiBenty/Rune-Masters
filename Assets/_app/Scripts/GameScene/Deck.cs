using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    private Image Visual;
    public List<Card> DeckList;

    // Start is called before the first frame update
    void Start()
    {
        Visual = GetComponent<Image>();
        DeckList = CardDatabase.Instance.toList(CardDatabase.Instance.cardService.GetCards());
        Shuffle();
    }

    void Update()
    {
        if (DeckList.Count == 0)
        {
            Visual.enabled = false;
        }
    }

    public void Shuffle(){
        DeckList = DeckList.OrderBy(_ => Guid.NewGuid()).ToList();
    }

}
