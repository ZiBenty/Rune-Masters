using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance {get; private set;}
    public bool isPlayerTurn;
    public int playerTurn;
    public int enemyTurn;
    private GameManager _gm;

    private bool _initialSetup; //used to launch initial couroutines
    public static bool startGame;

    //used for switching between phases
    public bool isDrawPhase = false;
    public bool isMovePhase = false;
    public bool isCombatPhase = false;
    public bool isEndPhase = false;

    //delegates methods for handling moving phases events

    //public delegate void OnStartDrawPhaseDelegate();
    //public event OnStartDrawPhaseDelegate OnStartDrawPhase;

    public delegate void OnStartPlayerTurnDelegate();
    public event OnStartPlayerTurnDelegate OnStartPlayerTurn;
    public delegate void OnStartEnemyTurnDelegate();
    public event OnStartEnemyTurnDelegate OnStartEnemyTurn;

    //draw phase
    public delegate void OnStartDrawPhaseDelegate();
    public event OnStartDrawPhaseDelegate OnStartDrawPhase;

    //Move phase
    public delegate void OnStartMovePhaseDelegate();
    public event OnStartMovePhaseDelegate OnStartMovePhase;
    public delegate void OnEndMovePhaseDelegate();
    public event OnEndMovePhaseDelegate OnEndMovePhase;

    //combat phase
    public delegate void OnStartCombatPhaseDelegate();
    public event OnStartCombatPhaseDelegate OnStartCombatPhase;
    public delegate void OnEndCombatPhaseDelegate();
    public event OnEndCombatPhaseDelegate OnEndCombatPhase;

    public delegate void OnStartEndPhaseDelegate();
    public event OnStartEndPhaseDelegate OnStartEndPhase;

    void Awake(){
        if (Instance == null){
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        OnEnable();
    }

    public void OnEnable(){
        _gm = GameManager.Instance;
        isPlayerTurn = true;
        playerTurn = 1;
        enemyTurn = 0;
        startGame = true;
        _initialSetup = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_initialSetup && startGame){
            UIManager.Instance.UpdateTurnText("Player's Turn 1");
            StartCoroutine(_gm.Draw(_gm.player, 5));
            StartCoroutine(_gm.Draw(_gm.enemy, 5)); 
            _initialSetup = false;
        }
        if(startGame && _gm.enemy.handScript.transform.childCount == 5){
            _gm.enemy.handScript.setDraggable(false);
            startGame = false;
            isDrawPhase = true;
        }


        //Fase Pescata
        if (isDrawPhase){
            OnStartDrawPhase();
            if(isPlayerTurn)
                StartCoroutine(_gm.Draw(_gm.player, 1));
            else
                StartCoroutine(_gm.Draw(_gm.enemy, 1));
            //attiva effetti forgia
            movePhase();
        }
        //Fase Movimento
        if (isMovePhase){

        }
        //Fase Attacco
        if (isCombatPhase){

        }
        //Fine Turno
        if(isEndPhase){
            movePhase();
        }
    }

    public void movePhase(){
        if(isDrawPhase){
            isDrawPhase = false;
            isMovePhase = true;
            OnStartMovePhase();
        }
        else if(isMovePhase){
            OnEndMovePhase();
            isMovePhase = false;
            isCombatPhase = true;
            OnStartCombatPhase();
        }
        else if(isCombatPhase){
            OnEndCombatPhase();
            isCombatPhase = false;
            isEndPhase = true;
        }
        else if(isEndPhase){
            isEndPhase = false;
            onEndTurn();
        }
    }

    public void onEndTurn(){
        if(isMovePhase)
            OnEndMovePhase();
        else if(isCombatPhase)
            OnEndCombatPhase();
        OnStartEndPhase();
        if(isPlayerTurn){
            isPlayerTurn = false;
            enemyTurn ++;
            UIManager.Instance.UpdateTurnText("Enemy's Turn " + enemyTurn.ToString());
            _gm.player.handScript.setDraggable(false);
            _gm.enemy.handScript.setDraggable(true);
            OnStartEnemyTurn();
        }else{
            isPlayerTurn = true;
            playerTurn ++;
            UIManager.Instance.UpdateTurnText("Player's Turn " + playerTurn.ToString());
            _gm.enemy.handScript.setDraggable(false);
            _gm.player.handScript.setDraggable(true);
            OnStartPlayerTurn();
        }
        isDrawPhase = true;
    }
}
