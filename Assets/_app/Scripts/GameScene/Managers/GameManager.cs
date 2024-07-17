using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static Constants;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Player player;
    public Player enemy;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    // Game Actions, general actions like Draw, Destroy, Remove, Discard, ecc
    public IEnumerator Draw(Player player, int count){
        Deck deck = player.deckScript;
        Hand hand = player.handScript;
        if(count >= 1 && deck.DeckList.Count >= count){
            for (int i=0; i<count; i++){
                hand.AddCard(deck.DeckList[0]);
                deck.DeckList.RemoveAt(0);
                yield return new WaitForSeconds(0.2f);
            }    
        }
    }
/*
    public IEnumerator Move(Location destination){
        if
    }*/
}
