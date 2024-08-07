using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Constants;

[System.Serializable]
public class MyGameObjectEvent: UnityEvent<GameObject>{}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Player player;
    public Player enemy;
    private TurnSystem _ts;
    private TargetHandler _th;

    void Awake(){
        if (Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        OnEnable();
    }

    public void OnEnable(){
        player = GameObject.Find("Player").GetComponent<Player>();
        enemy = GameObject.Find("Enemy").GetComponent<Player>();
        _ts = TurnSystem.Instance;
        _th = TargetHandler.Instance;
        //can only be called once since crystals remain on field
        SubscribeToCrystals();
    }

    public void SubscribeToCrystals(){
        foreach(HealthComponent cardHp in FindObjectsOfType<HealthComponent>()){
            if(cardHp.gameObject.GetComponent<CardInfo>().TempInfo.Id == 0)
                cardHp.OnDestruction.AddListener(OnCrystalDestruction);
        }
        
    }

    public void OnCrystalDestruction(GameObject crystal){
        //enemy lost
        if(crystal.GetComponent<CardState>().Owner.transform.name == "Enemy"){
            UIManager.Instance.VictoryPanelActivate("Player Won");
        }//player lost
        else{
            UIManager.Instance.VictoryPanelActivate("Enemy Won");
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

    
    public IEnumerator MoveLocation(GameObject card, Location destination){
        Player p = card.GetComponent<CardState>().Controller;
        Deck deck;
        //Hand hand;
        switch(destination){
            case Location.Discard:
                DiscardZone discardZone = p.discardScript;
                if(card.GetComponent<CardState>().Location == Location.Field){
                    GameObject parent = card.transform.parent.gameObject;
                    discardZone.AddCard(card);
                    Destroy(parent);
                    Destroy(card);
                }else if(card.GetComponent<CardState>().Location == Location.Hand){
                    GameObject parent = card.transform.parent.gameObject;
                    discardZone.AddCard(card);
                    Destroy(parent);
                    Destroy(card);
                }else if(card.GetComponent<CardState>().Location == Location.Deck){
                    deck = p.deckScript;
                    int deckIndex = deck.DeckList.IndexOf(card);
                    discardZone.AddCard(card);
                    deck.DeckList.RemoveAt(deckIndex);
                }
                break;
            case Location.Deck:
                deck = p.deckScript;
                if(card.GetComponent<CardState>().Location == Location.Discard){
                    deck.AddCard(card);
                }
                break;
        }
        yield return null;
    }

   public void Target(Player player, int numTargets){
        _th.StartTargetMode(numTargets, false);
   }

    //functions made for testing (DELETE LATER)
    public void onDraw(){
        if(_ts.isPlayerTurn)
            StartCoroutine(Draw(player, 1));
        else
            StartCoroutine(Draw(enemy, 1));
    }

    public IEnumerator DestroyTarget(Player player, int numTargets, string hintText){
        UIManager.Instance.ChangeHintBox(true, hintText);
        _th.OnAddTarget += CheckOnDestroy;
        Target(player, numTargets);
        yield return new WaitUntil(() => !_th.TargetMode);
        _th.OnAddTarget -= CheckOnDestroy;
        UIManager.Instance.ChangeHintBox(false);
        StartCoroutine(MoveLocation(_th.Targets[0], Location.Discard));
    }

    public void onDestroyButton(){
        if(_ts.isPlayerTurn)
            StartCoroutine(DestroyTarget(player, 1, "Choose card to destroy"));
        else
            StartCoroutine(DestroyTarget(enemy, 1, "Choose card to destroy"));
    }

    public bool CheckOnDestroy(GameObject target){
        if(target.TryGetComponent<CardState>(out var state)){
            if(state.Location == Location.Field){
                return true;
            }
            else{
                return false;
            }
        }
        return false;
    }

}
