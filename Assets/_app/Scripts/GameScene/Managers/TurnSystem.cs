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
    [SerializeField]
    private TMP_Text _turnText;
    private GameManager _gm;

    private bool _initialSetup; //used to launch initial couroutines
    public static bool startGame;

    //used for switching between phases
    public bool isDrawPhase = false;
    public bool isMovePhase = false;
    public bool isCombatPhase = false;
    public bool isEndPhase = false;

    //delegates methods for handling moving phases events
    public delegate void OnStartMainPhaseDelegate();
    public event OnStartMainPhaseDelegate OnStartMainPhase;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _gm = GameManager.Instance;
        isPlayerTurn = true;
        playerTurn = 1;
        enemyTurn = 0;
        _turnText.text = "Player's Turn 1";
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
            isDrawPhase = true;
        }


        //Fase Pescata
        if (isDrawPhase){
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
            OnStartMainPhase();
        }
        else if(isMovePhase){
            isMovePhase = false;
            isCombatPhase = true;
        }
        else if(isCombatPhase){
            isCombatPhase = false;
            isEndPhase = true;
        }
        else if(isEndPhase){
            isEndPhase = false;
            onEndTurn();
        }
    }

    public void onEndTurn(){
        if(isPlayerTurn){
            isPlayerTurn = false;
            enemyTurn ++;
            _turnText.text = "Enemy's Turn " + enemyTurn.ToString();
            _gm.player.handScript.setDraggable(false);
            _gm.enemy.handScript.setDraggable(true);
        }else{
            isPlayerTurn = true;
            playerTurn ++;
            _turnText.text = "Player's Turn " + playerTurn.ToString();
            _gm.enemy.handScript.setDraggable(false);
            _gm.player.handScript.setDraggable(true);
        }
        isDrawPhase = true;
    }
}
