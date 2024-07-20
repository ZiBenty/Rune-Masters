using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

//Handles logic for casting card from your hand and determining costs
public class CastComponent : MonoBehaviour
{
    public List<List<Rune>> CostMatrix;
    private List<ArenaLine> _arena;
    public List<Vector2> ValidCastingPatterns;
    public bool CanBeCastedValue = false;

    List<Slot> ColoredSlots;

    void Start(){
        SetCostMatrix();
        _arena = GameObject.Find("Arena").GetComponent<Arena>().Lines;
        ColoredSlots = new List<Slot>();
    }

    public void SetCostMatrix(){
        List<Rune> cost = transform.GetComponent<CardInfo>().TempInfo.DecodeCost();
        CostMatrix = new List<List<Rune>>
        {
            new List<Rune>(),
            new List<Rune>()
        };
        int i=0;
        foreach(List<Rune> list in CostMatrix){
            list.Add(cost[i]);
            list.Add(cost[i+1]);
            list.Add(cost[i+2]);
            i=3;
        }
    }

    //returns true if tempRune on slot can be used for the rune in casting cost, false otherwise
    private bool checkRune(List<Rune> runesOnSlot, Rune castingRune){
        if (runesOnSlot.Count == 0)
            return false;
        foreach(Rune rune in runesOnSlot){
            Rune check = castingRune & rune; //bitwise operation to check if rune is present
            if (check == Rune.None) //does not match
                return false;
        }
        return true;
    }

    private void IterateCost(string player, int lineIndex, int columnIndex){
        int checkCost = 0; //if it reaches 6, then we found a casting pattern

        for (int costLineIndex = 0; costLineIndex < 2; costLineIndex++){
            int flip=0;
            if (costLineIndex == 0){
                switch (player){
                    case "Player":
                        flip = 1;
                        break;
                    case "Enemy":
                        flip = -1;
                        break;
                }
            }
            else{
                flip = 0;
            }
                
            for (int costColumnIndex = 0; costColumnIndex < 3; costColumnIndex++){
                if (CostMatrix[costLineIndex][costColumnIndex] == Rune.None){
                    checkCost++;
                    continue; //dont need to check
                }
                else{
                    int columnAdjuster = 0;
                    switch (player){
                        case "Player":
                            columnAdjuster = costColumnIndex;
                            break;
                        case "Enemy":
                            columnAdjuster = costColumnIndex*-1;
                            break;
                    }
                    //check if TempRune are Owned by the same player as the card Controller
                    if(_arena[lineIndex+flip].transform.GetChild(columnIndex+columnAdjuster).GetChild(1).GetComponent<TempRunes>().Owner == transform.GetComponent<CardState>().Controller)
                        if(checkRune(_arena[lineIndex+flip].transform.GetChild(columnIndex+columnAdjuster).GetChild(1).GetComponent<TempRunes>().TempRunesList, CostMatrix[costLineIndex][costColumnIndex]))
                            checkCost++;
                }
            }
        }

        //if casting pattern was found, save topLeft corner
        if (checkCost == 6){
            int flip = 0;
            switch (player){
                case "Player":
                    flip = 1;
                    break;
                case "Enemy":
                    flip = -1;
                    break;
            }
            Slot topLeft = _arena[lineIndex+flip].transform.GetChild(columnIndex).GetComponent<Slot>();
            ValidCastingPatterns.Add(topLeft.Coordinates);
            CanBeCastedValue = true; //we found a patter, so card can be casted
        }
    }

    public bool CanBeCasted(){
        ValidCastingPatterns = new List<Vector2>(); //resets the casting patterns
        //checks by boxes of 3x2 and saves top-left position in ValidCastingPatterns
        string player;
        //Controller: Player
        if (transform.GetComponent<CardState>().Controller.transform.gameObject == GameObject.Find("Player")){
            player = "Player";
            for (int lineIndex = 0; lineIndex < _arena.Count-1; lineIndex++){
                for (int columnIndex = 0; columnIndex < 3; columnIndex++){
                    IterateCost(player, lineIndex, columnIndex);
                }
            }
        } else{ //Controller: Enemy
            player = "Enemy";
            //since Enemy sees the arena from the other side, we need to iterate the arena Slots in the opposite order
            for (int lineIndex = 4; lineIndex > 0; lineIndex--){
                for (int columnIndex = 4; columnIndex > 1; columnIndex--){
                    IterateCost(player, lineIndex, columnIndex);
                }
            }
        }
        return CanBeCastedValue;
    }

    private IEnumerator CastingProcedure(){
        if (transform.GetComponent<CardState>().Controller.transform.gameObject == GameObject.Find("Player"))
            UIManager.Instance.ChangeHintBox(true, "Choose Top-Left Slot of Runes you want to sacrifice");
        else
            UIManager.Instance.ChangeHintBox(true, "Choose Bottom-Right Slot of Runes you want to sacrifice");
        TargetHandler.Instance.OnAddTarget += CheckTargetSlots;
        TargetHandler.Instance.StartTargetMode(1, true);
        ShowTopLeftPatterns();
        yield return new WaitUntil(() => !TargetHandler.Instance.TargetMode);
        TargetHandler.Instance.OnAddTarget -= CheckTargetSlots;
        DeColorPatternOnArena();
        UIManager.Instance.ChangeHintBox(false);
        //last check before deletion
        if (ValidCastingPatterns.Contains(TargetHandler.Instance.Targets[0].transform.parent.GetComponent<Slot>().Coordinates)){
            SacrificeCardsAndRunes();
            transform.GetComponent<PlayScript>().PlaceInSlot();
        }
    }

    public bool CheckTargetSlots(GameObject target){
        if(target.transform.parent.TryGetComponent<Slot>(out var slot)){
            if (ValidCastingPatterns.Contains(slot.Coordinates)){
                ColorPatternOnArena(slot.Coordinates);
                return true;
            }
        }
        return false;
    }

    //colors all possible patterns
    public void ShowTopLeftPatterns(){
        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Vector2 coord in ValidCastingPatterns){
            foreach (Slot slot in slots){
                if (slot.Coordinates.x == coord.x && slot.Coordinates.y == coord.y){
                    slot.transform.GetChild(0).GetComponent<ArenaCardSlot>()._outline.enabled = true;
                }
            }
        }
    }

    public void ColorPatternOnArena(Vector2 TopLeft){ 
        if (ColoredSlots.Count != 0)
            DeColorPatternOnArena();

        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots){
            if (slot.Coordinates.x <= TopLeft.x && slot.Coordinates.x > TopLeft.x-2 &&
            slot.Coordinates.y >= TopLeft.y && slot.Coordinates.y < TopLeft.y+3){
                ColoredSlots.Add(slot);
                slot.transform.GetChild(0).GetComponent<ArenaCardSlot>()._outline.enabled = true;
            }
        }
    }

    public void DeColorPatternOnArena(){
        foreach(Slot slot in ColoredSlots){
            slot.transform.GetChild(0).GetComponent<ArenaCardSlot>()._outline.enabled = false;
        }
        ColoredSlots.Clear();
    }

    public void SacrificeCardsAndRunes(){
        foreach(Slot slot in ColoredSlots){
            if(slot.transform.GetChild(0).childCount != 0){ //if a card is present to be sacrificed
                GameObject card = slot.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                if(card.GetComponent<CardInfo>().TempInfo.Id != 0){ //crystal it's not sacrificed for summons
                    GameManager.Instance.MoveLocation(card, Location.Discard);
                }
            }
        }
    }

    //returns true if everything went smoothly, otherwise returns false
    public bool CastCard(){
        if (!CanBeCastedValue) return false;
        //start casting procedure
        StartCoroutine(CastingProcedure());
        return true;
    }
}
