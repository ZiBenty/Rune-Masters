using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

//Handles logic for casting card from your hand and determining costs
public class CastComponent : MonoBehaviour
{
    public List<Vector2> ValidCastingPatterns;

    public bool CanBeCastedValue = false;

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

    public List<List<Rune>> GetCostMatrix(){
        List<Rune> cost = transform.GetComponent<CardInfo>().TempInfo.DecodeCost();
        List<List<Rune>> cst = new List<List<Rune>>
        {
            new List<Rune>(),
            new List<Rune>()
        };
        int i=0;
        foreach(List<Rune> list in cst){
            list.Add(cost[i]);
            list.Add(cost[i+1]);
            list.Add(cost[i+2]);
            i=3;
        }
        return cst;
    }

    public bool CanBeCasted(){
        ValidCastingPatterns = new List<Vector2>(); //resets the casting patterns
        List<List<Rune>> cst = GetCostMatrix();
        List<ArenaLine> arena = GameObject.Find("Arena").GetComponent<Arena>().Lines;
        //checks by boxes of 3x2 and saves top-left position in ValidCastingPatterns
        //Controller: Player
        if (transform.GetComponent<CardState>().Controller.transform.gameObject == GameObject.Find("Player")){
            for (int lineIndex = 0; lineIndex < arena.Count-1; lineIndex++){
                for (int columnIndex = 0; columnIndex < 3; columnIndex++){

                    int checkCost = 0; //if it reaches 6, then we found a casting pattern
                    for (int costLineIndex = 0; costLineIndex < 2; costLineIndex++){
                        int flip;
                        if (costLineIndex == 0)//We need to check lineIndex+1, then lineIndex
                            flip = 1;
                        else
                            flip = 0;
                        for (int costColumnIndex = 0; costColumnIndex < 3; costColumnIndex++){
                            if (cst[costLineIndex][costColumnIndex] == Rune.None){
                                checkCost++;
                                continue; //dont need to check
                            }
                            else{
                                //check if TempRune are Owned by the same player as the card Controller
                                if(arena[lineIndex+flip].transform.GetChild(columnIndex+costColumnIndex).GetChild(1).GetComponent<TempRunes>().Owner == transform.GetComponent<CardState>().Controller)
                                    if(checkRune(arena[lineIndex+flip].transform.GetChild(columnIndex+costColumnIndex).GetChild(1).GetComponent<TempRunes>().TempRunesList, cst[costLineIndex][costColumnIndex]))
                                        checkCost++;
                            }
                        }
                    }

                    //if casting pattern was found, save topLeft corner
                    if (checkCost == 6){
                        Slot topLeft = arena[lineIndex+1].transform.GetChild(columnIndex).GetComponent<Slot>();
                        ValidCastingPatterns.Add(topLeft.Coordinates);
                        CanBeCastedValue = true; //we found a patter, so card can be casted
                    }

                }
            }
        } else{ //Controller: Enemy
            for (int lineIndex = 4; lineIndex > 0; lineIndex--){
                for (int columnIndex = 4; columnIndex > 1; columnIndex--){

                    int checkCost = 0; //if it reaches 6, then we found a casting pattern
                    for (int costLineIndex = 0; costLineIndex < 2; costLineIndex++){
                        int flip;
                        if (costLineIndex == 0)//We need to check lineIndex+1, then lineIndex
                            flip = -1;
                        else
                            flip = 0;
                        for (int costColumnIndex = 0; costColumnIndex < 3; costColumnIndex++){
                            if (cst[costLineIndex][costColumnIndex] == Rune.None){
                                checkCost++;
                                continue; //dont need to check
                            }
                            else{
                                //check if TempRune are Owned by the same player as the card Controller
                                if(arena[lineIndex+flip].transform.GetChild(columnIndex-costColumnIndex).GetChild(1).GetComponent<TempRunes>().Owner == transform.GetComponent<CardState>().Controller)
                                    if(checkRune(arena[lineIndex+flip].transform.GetChild(columnIndex-costColumnIndex).GetChild(1).GetComponent<TempRunes>().TempRunesList, cst[costLineIndex][costColumnIndex]))
                                        checkCost++;
                            }
                        }
                    }

                    //if casting pattern was found, save topLeft corner
                    if (checkCost == 6){
                        Slot topLeft = arena[lineIndex-1].transform.GetChild(columnIndex).GetComponent<Slot>();
                        ValidCastingPatterns.Add(topLeft.Coordinates);
                        CanBeCastedValue = true; //we found a patter, so card can be casted
                    }

                }
            }
        }
        return CanBeCastedValue;
    }

    //returns true if everything went smoothly, otherwise returns false
    public bool CastCard(){
        return false;
    }
}
