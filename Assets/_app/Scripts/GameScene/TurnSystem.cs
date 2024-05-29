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
    // Start is called before the first frame update
    void Start()
    {
        isPlayerTurn = true;
        playerTurn = 1;
        enemyTurn = 0;
        TurnText.text = "Player's Turn";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onEndTurn(){
        if(isPlayerTurn){
            isPlayerTurn = false;
            enemyTurn += 1;
            TurnText.text = "Enemy's Turn";
        }else{
            isPlayerTurn = true;
            playerTurn += 1;
            TurnText.text = "Player's Turn";
        }
    }

    public void onDraw(){
        if(isPlayerTurn)
            GameController.Instance.player.Draw(1);
        else
            GameController.Instance.enemy.Draw(1);
    }
}
