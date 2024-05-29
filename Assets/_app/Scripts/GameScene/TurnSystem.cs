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

    public static bool startGame;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;
        playerTurn = 1;
        enemyTurn = 0;
        TurnText.text = "Player's Turn 1";
        startGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startGame){
            StartCoroutine(GameManager.Instance.player.Draw(5));
            StartCoroutine(GameManager.Instance.enemy.Draw(5));
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
        }else{
            isPlayerTurn = true;
            playerTurn ++;
            TurnText.text = "Player's Turn " + playerTurn.ToString();
        }
    }

    public void onDraw(){
        if(isPlayerTurn)
            StartCoroutine(GameManager.Instance.player.Draw(1));
        else
            StartCoroutine(GameManager.Instance.enemy.Draw(1));
    }
}
