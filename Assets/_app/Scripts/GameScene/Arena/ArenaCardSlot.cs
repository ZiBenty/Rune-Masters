using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ArenaCardSlot : MonoBehaviour
{
    private Outline _outline;
    public GameObject cardPrefab;
    public GameObject visualPrefab;
    public bool isCrystalSlot = false;
    public Player Owner;
    public Vector2 ContainerSize = new Vector2(50, 60);


    // Start is called before the first frame update
    void Start()
    {
        _outline = GetComponent<Outline>();
        if (isCrystalSlot){
            Card c = CardDatabase.Instance.cardService.GetCardFromId(0);
            GameObject card = Instantiate(cardPrefab);
            card.GetComponent<CardInfo>().LoadInfo(c);
            card.GetComponent<CardState>().SetPlayer(Owner);
            PlaceCard(card, false);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.name.Contains("Card")){
            if (other.transform.GetComponent<PlayScript>().GetisDragging())
                _outline.enabled = true;
        } 
    }

    void OnTriggerExit2D(Collider2D other){
        _outline.enabled = false;
    }

    public void PlaceCard(GameObject card, bool cardWasMoved = true){
        //creates CardContainer object
        GameObject container = new GameObject("CardContainer");
        container.AddComponent<RectTransform>();
        container.transform.SetParent(transform, false);
        container.GetComponent<RectTransform>().sizeDelta = ContainerSize;
        HorizontalLayoutGroup hzl = container.AddComponent<HorizontalLayoutGroup>();
        hzl.childControlHeight = true;
        hzl.childControlWidth = true;

        GameObject copy;
        if(cardWasMoved){
            copy = Instantiate(card);
            copy.GetComponent<CardInfo>().LoadInfo(card.GetComponent<CardInfo>().BaseInfo);
        }else{
            copy = card;
        }
        if (copy.transform.childCount != 0)
            Destroy(copy.transform.GetChild(0).gameObject); // removes visual from copy object
        copy.transform.SetParent(container.transform, false);
        

        //instantiate object and places it in the new CardContainer
        GameObject cv = Instantiate(visualPrefab, copy.transform);
        cv.transform.localScale = new Vector3(0.16f, 0.16f);
        container.transform.localPosition = Vector3.back;
        cv.GetComponent<CardDisplay>().LoadCard();

        copy.GetComponent<CardState>().Location = Constants.Location.Field;
        //creates a tempRune in this slot
        transform.parent.GetChild(1).GetComponent<TempRunes>().CreateTempRune(copy.GetComponent<CardInfo>().TempInfo.CardRune);
    }
        
}

