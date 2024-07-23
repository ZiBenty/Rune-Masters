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
    public GameObject DiscardZone;
    public DiscardZone discardScript;
    public GameObject SummonLine;
    public List<GameObject> cardsOnField;
    [SerializeField]
    private GameObject Crystal;

    void Start()
    {
        deckScript = Deck.GetComponent<Deck>();
        handScript = Hand.GetComponent<Hand>();
        discardScript = DiscardZone.GetComponent<DiscardZone>();
        Crystal = SummonLine.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).gameObject;
        cardsOnField.Add(Crystal);
        if (SummonLine.transform.name == "Summon Player Line"){
            TurnSystem.Instance.OnStartPlayerTurn += StartOwnTurn;
            TurnSystem.Instance.OnStartEnemyTurn += StartOpponentTurn;
        }else{
            TurnSystem.Instance.OnStartPlayerTurn += StartOpponentTurn;
            TurnSystem.Instance.OnStartEnemyTurn += StartOwnTurn;
        }
    }

    public GameObject GetCrystal(){
        return Crystal;
    }

    public void StartOwnTurn(){
        SetCardsOnFieldDraggable(true);
    }

    public void StartOpponentTurn(){
        SetCardsOnFieldDraggable(false);
    }

    public void SetCardsOnFieldDraggable(bool b){
        foreach(var card in cardsOnField){
            if(card.GetComponent<CardInfo>().TempInfo.Id != 0)
                card.GetComponent<PlayScript>().SetcanDrag(b);
        }
    }

    public void SetCardsOnFieldInspectable(bool b){
        foreach(var card in cardsOnField){
            if(card.GetComponent<CardInfo>().TempInfo.Id != 0)
                card.GetComponent<PlayScript>().SetcanInspect(b);
        }
    }

    void Update()
    {

    }
    
}
