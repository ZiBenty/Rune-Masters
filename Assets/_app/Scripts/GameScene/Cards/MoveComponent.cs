using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public List<Slot> AvailableSlots;
    private List<ArenaLine> _arena;
    public bool CanBeMoved = true;

    // Start is called before the first frame update
    void Start()
    {
        TurnSystem.Instance.OnStartMainPhase += SetCanBeMoved;
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
            if (ownCoord.y != 4){
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
            if (ownCoord.y != 0){
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

    void OnDestroy(){
        TurnSystem.Instance.OnStartMainPhase -= SetCanBeMoved;
    }
}
