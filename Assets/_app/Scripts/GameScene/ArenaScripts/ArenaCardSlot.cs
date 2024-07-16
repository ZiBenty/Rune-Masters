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


    // Start is called before the first frame update
    void Start()
    {
        _outline = GetComponent<Outline>();
        if (isCrystalSlot){
            Card c = CardDatabase.Instance.cardService.GetCardFromId(0);
            GameObject card = Instantiate(cardPrefab);
            card.GetComponentInChildren<CardInfo>().LoadInfo(c);
            card.GetComponentInChildren<CardState>().SetPlayer(Owner);
            card.GetComponentInChildren<CardState>().Location = Constants.Location.Field;
            card.GetComponent<PlayScript>().SetcanDrag(false);
            card.GetComponent<BoxCollider2D>().enabled = true;
            PlaceCard(card, false, false);
            card.GetComponent<PlayScript>().SetcanInspect(true);
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

    public void PlaceCard(GameObject card, bool changePlay = true, bool cardWasMoved = true){
        //creates CardContainer object
        GameObject container = new GameObject("CardContainer");
        container.AddComponent<RectTransform>();
        container.transform.SetParent(transform, false);
        //container.transform.localPosition = Vector3.zero;
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 60);
        HorizontalLayoutGroup hzl = container.AddComponent<HorizontalLayoutGroup>();
        hzl.childControlHeight = true;
        hzl.childControlWidth = true;

        GameObject copy;
        if(cardWasMoved){
            copy = Instantiate(card);
            copy.GetComponentInChildren<CardInfo>().LoadInfo(card.GetComponentInChildren<CardInfo>().BaseInfo);
        }else{
            copy = card;
        }
        if (copy.transform.GetChild(1).transform.childCount != 0)
            Destroy(copy.transform.GetChild(1).transform.GetChild(0).gameObject); // removes visual from copy object
        copy.transform.SetParent(container.transform, false);
        

        //instantiate object and places it in the new CardContainer
        GameObject cv = Instantiate(visualPrefab, copy.transform.GetChild(1).transform);
        cv.transform.localScale = new Vector3(0.16f, 0.16f);
        container.transform.localPosition = Vector3.back;
        cv.GetComponent<CardDisplay>().LoadCard();

        copy.GetComponentInChildren<CardState>().Location = Constants.Location.Field;
        //enables colliders and size it to the visual size
        copy.GetComponent<BoxCollider2D>().enabled = true;
        copy.GetComponent<BoxCollider2D>().size = copy.transform.GetChild(1).GetComponentInChildren<RectTransform>().sizeDelta*new Vector3(0.16f, 0.16f, 0.16f);
        copy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(changePlay){
            copy.GetComponent<PlayScript>().SetcanDrag(true);
            copy.GetComponent<PlayScript>().SetcanInspect(true);
        }
        
    }
}
