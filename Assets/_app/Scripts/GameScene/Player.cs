using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Deck;
    public Deck deckScript;
    public GameObject Hand;
    public Hand handScript;
    public GameObject SummonLine;
    public List<GameObject> cardsOnField;
    private int Life {get; set;}

    void Start()
    {
        deckScript = Deck.GetComponent<Deck>();
        handScript = Hand.GetComponent<Hand>();
        cardsOnField.Add(SummonLine.transform.GetChild(2).GetChild(0).gameObject);
    }

    void Update()
    {
        
    }
    
}
