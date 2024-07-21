using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class AttackComponent : MonoBehaviour
{
    private int m_Atk;
    public int Atk{
        get { return m_Atk; }
        set{
            if (m_Atk == value) return;
            if (value > 10)
                m_Atk = 10;
            else
                m_Atk = value;
            if (OnAtkChange != null)
                OnAtkChange(m_Atk);
        }
    }
    public delegate void OnAtkChangeDelegate(int atk);
    public event OnAtkChangeDelegate OnAtkChange;

    public List<Slot> SlotsAttackTargets;
    private List<ArenaLine> _arena;
    public bool CanAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        Atk = transform.GetComponent<CardInfo>().TempInfo.Atk;
        OnAtkChange += AttackChange;
        TurnSystem.Instance.OnStartCombatPhase += SetStartCombatPhase;
        TurnSystem.Instance.OnEndCombatPhase += SetEndCombatPhase;
        TurnSystem.Instance.OnStartEndPhase += SetEndCombatPhase;
        _arena = GameObject.Find("Arena").GetComponent<Arena>().Lines;
    }

    private void AttackChange(int atk){
        transform.GetComponent<CardInfo>().TempInfo.Atk = atk;
        transform.GetChild(0).GetComponent<CardDisplay>().LoadCard();
    }

    public bool CanItAttack(){
        if (!CanAttack) return false;
        FindAttackTargets();
        if (SlotsAttackTargets.Count == 0)
            CanAttack = false;
        return CanAttack;
    }

    private void FindAttackTargets(){
        SlotsAttackTargets = new List<Slot>();
        Vector2 ownCoord = transform.parent.parent.parent.GetComponent<Slot>().Coordinates;
        //checks for card owned by player/enemy
        //check on left/right
        if (ownCoord.y != 0){
            Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y-1).GetComponent<Slot>();
            //checks if slot on their left is empty
            if (slot.transform.GetChild(0).childCount != 0 && slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardState>().Controller != transform.GetComponent<CardState>().Controller)
                SlotsAttackTargets.Add(slot);
        }
        //check on right/left
        if (ownCoord.y != 4){
            Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y+1).GetComponent<Slot>();
            //checks if slot on their left is empty
            if (slot.transform.GetChild(0).childCount != 0 && slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardState>().Controller != transform.GetComponent<CardState>().Controller)
                SlotsAttackTargets.Add(slot);
        }
        //check forward/backward
        if (ownCoord.x != 4){
            Slot slot = _arena[(int)ownCoord.x+1].transform.GetChild((int)ownCoord.y).GetComponent<Slot>();
            //checks if slot on their left is empty
            if (slot.transform.GetChild(0).childCount != 0 && slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardState>().Controller != transform.GetComponent<CardState>().Controller)
                SlotsAttackTargets.Add(slot);
        }
        //check backward/forward
        if (ownCoord.x != 0){
            Slot slot = _arena[(int)ownCoord.x-1].transform.GetChild((int)ownCoord.y).GetComponent<Slot>();
            //checks if slot on their left is empty
            if (slot.transform.GetChild(0).childCount != 0 && slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardState>().Controller != transform.GetComponent<CardState>().Controller)
                SlotsAttackTargets.Add(slot);
        }
    }

    public void ColorAttackTargets(bool b){
        foreach(Slot slot in SlotsAttackTargets){
            slot.transform.GetChild(0).GetComponent<ArenaCardSlot>().OutlineComp.enabled = b;
        }
    }

    public IEnumerator AttackProcedure(){
        UIManager.Instance.ChangeHintBox(true, "Choose card you want to attack");
        TargetHandler.Instance.OnAddTarget += CheckAttackTarget;
        TargetHandler.Instance.StartTargetMode(1, true);
        _arena[0].transform.parent.GetComponent<Arena>().DeColorSlots();
        ColorAttackTargets(true);
        EnableInspectAttackTargets(false);
        yield return new WaitUntil(() => !TargetHandler.Instance.TargetMode);
        TargetHandler.Instance.OnAddTarget -= CheckAttackTarget;
        _arena[0].transform.parent.GetComponent<Arena>().DeColorSlots();
        UIManager.Instance.ChangeHintBox(false);
        EnableInspectAttackTargets(true);
        if (TargetHandler.Instance.Targets.Count != 0){
            if (SlotsAttackTargets.Contains(TargetHandler.Instance.Targets[0].transform.parent.GetComponent<Slot>())){
                GameObject card = TargetHandler.Instance.Targets[0].transform.GetChild(0).GetChild(0).gameObject;
                card.transform.GetComponent<HealthComponent>().Hp -= Atk;
                CanAttack = false; //one attack per turn
            }
        }
    }

    public bool CheckAttackTarget(GameObject target){
        if(target.transform.parent.TryGetComponent<Slot>(out var slot)){
            if (SlotsAttackTargets.Contains(slot)){
                return true;
            }
        }
        return false;
    }

    private void EnableInspectAttackTargets(bool b){
        foreach(Slot slot in SlotsAttackTargets){
            if (slot.transform.GetChild(0).transform.childCount != 0){
                slot.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<BoxCollider2D>().enabled=b;
            }
        }
    }

    public void Attack(){
        if (!CanAttack) return;
        StartCoroutine(AttackProcedure());
    }

    public void SetCanAttack(bool b){
        CanAttack = b;
    }

    public void SetStartCombatPhase(){
        if ((transform.GetComponent<CardState>().Controller.transform.name == "Player" && TurnSystem.Instance.isPlayerTurn) ||
        (transform.GetComponent<CardState>().Controller.transform.name == "Enemy" && !TurnSystem.Instance.isPlayerTurn))
            if (transform.GetComponent<CardState>().Location == Location.Field)
                SetCanAttack(true);
    }

    public void SetEndCombatPhase(){
        if ((transform.GetComponent<CardState>().Controller.transform.name == "Player" && TurnSystem.Instance.isPlayerTurn) ||
        (transform.GetComponent<CardState>().Controller.transform.name == "Enemy" && !TurnSystem.Instance.isPlayerTurn))
            if (transform.GetComponent<CardState>().Location == Location.Field)
                SetCanAttack(false);
    }

    void OnDestroy(){
        TurnSystem.Instance.OnStartCombatPhase -= SetStartCombatPhase;
        TurnSystem.Instance.OnEndCombatPhase -= SetEndCombatPhase;
        TurnSystem.Instance.OnStartEndPhase -= SetEndCombatPhase;
    }
}
