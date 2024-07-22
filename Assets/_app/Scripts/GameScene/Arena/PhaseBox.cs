using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

//Logic handling the behaviour of the boxes that indicate the current turn phase
public class PhaseBox : MonoBehaviour
{
    private TurnSystem _ts;
    private string _phaseName;
    private bool _isPhase = false;

    void Start(){
        _ts = TurnSystem.Instance;
        _phaseName = transform.parent.name;
        if (_phaseName == "DrawPhase"){
            _ts.OnStartDrawPhase += ChangeState;
            _ts.OnStartMovePhase += ChangeState;
        }       
        else if (_phaseName == "MovePhase"){
            _ts.OnStartMovePhase += ChangeState;
            _ts.OnEndMovePhase += ChangeState;
        }
        else if (_phaseName == "CombatPhase"){
            _ts.OnStartCombatPhase += ChangeState;
            _ts.OnEndCombatPhase += ChangeState;
        }
        else if (_phaseName == "EndPhase"){
            _ts.OnStartEndPhase += ChangeState;
            _ts.OnStartPlayerTurn += ChangeState;
            _ts.OnStartEnemyTurn += ChangeState;
        }
    }

    public void ChangeState(){
        _isPhase = !_isPhase;
        if(_isPhase)
            GetComponent<Image>().color = Color.green;
        else
            GetComponent<Image>().color = Color.white;
    }

    void OnDestroy(){
        if (_phaseName == "DrawPhase"){
            _ts.OnStartDrawPhase -= ChangeState;
            _ts.OnStartMovePhase -= ChangeState;
        }       
        else if (_phaseName == "MovePhase"){
            _ts.OnStartMovePhase -= ChangeState;
            _ts.OnEndMovePhase -= ChangeState;
        }
        else if (_phaseName == "CombatPhase"){
            _ts.OnStartCombatPhase -= ChangeState;
            _ts.OnEndCombatPhase -= ChangeState;
        }
        else if (_phaseName == "EndPhase"){
            _ts.OnStartEndPhase -= ChangeState;
            _ts.OnStartPlayerTurn -= ChangeState;
            _ts.OnStartEnemyTurn -= ChangeState;
        }
    }
}
