using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public bool isPlayerTurn;
    public int playerTurn;
    public int enemyTurn;
    [SerializeField]
    private TMP_Text TurnText;
    private GameManager _gm;

    public static bool startGame;
    private bool _initialSetup; //used to launch initial couroutines

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instance;
        isPlayerTurn = true;
        playerTurn = 1;
        enemyTurn = 0;
        TurnText.text = "Player's Turn 1";
        startGame = true;
        _initialSetup = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_initialSetup && startGame){
            StartCoroutine(_gm.Draw(_gm.player, 5));
            StartCoroutine(_gm.Draw(_gm.enemy, 5)); 
            _initialSetup = false;
        }
        if(startGame && _gm.enemy.handScript.handVisual.Count == 5){
            _gm.enemy.handScript.setDraggable(false);
            startGame = false;
        }


        //Fase Pescata
        //Fase Movimento
        //Fase Attacco
        //Fine Turno
    }

    public void onStartTurn(){
        if(isPlayerTurn){

        }
    }

    public void onEndTurn(){
        if(isPlayerTurn){
            isPlayerTurn = false;
            enemyTurn ++;
            TurnText.text = "Enemy's Turn " + enemyTurn.ToString();
            _gm.player.handScript.setDraggable(false);
            _gm.enemy.handScript.setDraggable(true);
        }else{
            isPlayerTurn = true;
            playerTurn ++;
            TurnText.text = "Player's Turn " + playerTurn.ToString();
            _gm.enemy.handScript.setDraggable(false);
            _gm.player.handScript.setDraggable(true);
        }
    }

    public void onDraw(){
        if(isPlayerTurn)
            StartCoroutine(_gm.Draw(_gm.player, 1));
        else
            StartCoroutine(_gm.Draw(_gm.enemy, 1));
    }
}
