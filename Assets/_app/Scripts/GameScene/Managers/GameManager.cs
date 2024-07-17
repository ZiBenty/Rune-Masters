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
    private TurnSystem _ts;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        _ts = TurnSystem.Instance;
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

   public IEnumerator Target(Player player){
        bool targeted = false;
        while(!targeted){
            Ray ray = Camera.main.ScreenPointToRay(TouchManager.Instance.LastTouch.position.ReadValue());
            RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
            if (hit2d.collider != null && hit2d.collider.gameObject.GetComponent<IInspect>() != null && hit2d.collider.gameObject.GetComponent<IInspect>().GetcanInspect()){
                yield return hit2d.collider.gameObject;
            }
        }
   }

    //functions made for testing
    public void onDraw(){
        if(_ts.isPlayerTurn)
            StartCoroutine(Draw(player, 1));
        else
            StartCoroutine(Draw(enemy, 1));
    }

    public void onDestroyButton(){

    }

}
