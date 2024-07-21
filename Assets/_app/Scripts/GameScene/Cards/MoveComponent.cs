using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class MoveComponent : MonoBehaviour
{
    public List<Slot> AvailableSlots;
    private List<ArenaLine> _arena;
    public bool CanBeMoved = true;

    // Start is called before the first frame update
    void Start()
    {
        TurnSystem.Instance.OnStartMainPhase += SetStartMainPhase;
        TurnSystem.Instance.OnEndMainPhase += SetEndMainPhase;
        _arena = GameObject.Find("Arena").GetComponent<Arena>().Lines;
    }

    public bool CanItBeMoved(){
        if (!CanBeMoved) return false;
        FindAvailableSlots();
        if (AvailableSlots.Count == 0)
            CanBeMoved = false;
        return CanBeMoved;
    }

    private void FindAvailableSlots(){
        AvailableSlots = new List<Slot>();
        Vector2 ownCoord = transform.parent.parent.parent.GetComponent<Slot>().Coordinates;
        if (transform.GetComponent<CardState>().Controller.transform.name == "Player"){
            //check on left
            if (ownCoord.y != 0){
                Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y-1).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
            //check on right
            if (ownCoord.y != 4){
                Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y+1).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
            //check forward
            if (ownCoord.x != 4){
                Slot slot = _arena[(int)ownCoord.x+1].transform.GetChild((int)ownCoord.y).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
                
        }//Controller: Enemy
        else{
            //check on left
            if (ownCoord.y != 4){
                Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y+1).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
            //check on right
            if (ownCoord.y != 0){
                Slot slot = _arena[(int)ownCoord.x].transform.GetChild((int)ownCoord.y-1).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
            //check forward
            if (ownCoord.x != 0){
                Slot slot = _arena[(int)ownCoord.x-1].transform.GetChild((int)ownCoord.y).GetComponent<Slot>();
                //checks if slot on their left is empty
                if (slot.transform.GetChild(0).childCount == 0)
                    AvailableSlots.Add(slot);
            }
        }
    }

    public void ColorAvailableSlots(bool b){
        foreach(Slot slot in AvailableSlots){
            slot.transform.GetChild(0).GetComponent<ArenaCardSlot>().OutlineComp.enabled = b;
        }
    }

    public void SetCanBeMoved(bool b){
        CanBeMoved = b;
    }

    public void SetStartMainPhase(){
        if ((transform.GetComponent<CardState>().Controller.transform.name == "Player" && TurnSystem.Instance.isPlayerTurn) ||
        (transform.GetComponent<CardState>().Controller.transform.name == "Enemy" && !TurnSystem.Instance.isPlayerTurn))
            if (transform.GetComponent<CardState>().Location == Location.Field)
                SetCanBeMoved(true);
    }

    public void SetEndMainPhase(){
        if ((transform.GetComponent<CardState>().Controller.transform.name == "Player" && TurnSystem.Instance.isPlayerTurn) ||
        (transform.GetComponent<CardState>().Controller.transform.name == "Enemy" && !TurnSystem.Instance.isPlayerTurn))
            if (transform.GetComponent<CardState>().Location == Location.Field)
                SetCanBeMoved(false);
    }

    void OnDestroy(){
        TurnSystem.Instance.OnStartMainPhase -= SetStartMainPhase;
        TurnSystem.Instance.OnEndMainPhase -= SetEndMainPhase;
    }
}
