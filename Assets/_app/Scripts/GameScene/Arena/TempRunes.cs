using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class TempRunes : MonoBehaviour
{
    //sprites used for runes
    public List<Sprite> RuneSprites;
    /*
    0=None
    1=Fire
    2=Earth
    3=Air
    4=Water
    5=Ancestral
    */

    public List<Rune> TempRunesList;

    public List<GameObject> TempRunesVisualList;

    public Player Owner = null;

    // Start is called before the first frame update
    void Start()
    {
        TempRunesList = new List<Rune>();
        TurnSystem.Instance.OnStartMainPhase += CreateTempRune;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRune(Image image, Rune rune){
        switch(rune){
            case Rune.Fire:
                image.sprite = RuneSprites[1];
                break;
            case Rune.Earth:
                image.sprite = RuneSprites[2];
                break;
            case Rune.Air:
                image.sprite = RuneSprites[3];
                break;
            case Rune.Water:
                image.sprite = RuneSprites[4];
                break;
            case Rune.Ancestral:
                image.sprite = RuneSprites[5];
                break;
            default:
                image.sprite = RuneSprites[0];
                break;
        }
    }

    //Creates TempRune based on card in CardSlot
    public void CreateTempRune(){
        if(transform.parent.GetChild(0).childCount != 0){ //check if CardSlot has a card in it
            GameObject card = transform.parent.GetChild(0).GetChild(0).GetChild(0).gameObject; //Slot.CardSlot.CardContainer.Card
            Rune cardRune = card.GetComponent<CardInfo>().TempInfo.CardRune;
            Player p = card.GetComponent<CardState>().Controller;
            if(!TempRunesList.Contains(cardRune)){
                TempRunesList.Add(cardRune);
                Owner = p;
                createTempRuneVisual(cardRune);
            }
        }
    }

    public void CreateTempRune(Rune rune, Player p){
        if(!TempRunesList.Contains(rune)){
            TempRunesList.Add(rune);
            Owner = p;
            createTempRuneVisual(rune);
        }
    }

    public void RemoveTempRune(Rune rune){
        foreach(Rune placedRune in TempRunesList){
            Rune check = placedRune & rune;
            if (check != Rune.None){
                int index = TempRunesList.IndexOf(placedRune);
                TempRunesList.RemoveAt(index);
                Destroy(TempRunesVisualList[index]);
                TempRunesVisualList.RemoveAt(index);
                Owner = null;
                break;
            }
        }
    }

    private void createTempRuneVisual(Rune rune){
        GameObject runeBox = new GameObject("Rune");
        runeBox.AddComponent<Image>();
        runeBox.transform.SetParent(transform, false);
        ChangeRune(runeBox.GetComponent<Image>(), rune);
        TempRunesVisualList.Add(runeBox);
    }

    void OnDestroy(){
        TurnSystem.Instance.OnStartMainPhase -= CreateTempRune;
    }
}
