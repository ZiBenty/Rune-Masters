using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    private Outline _outline;
    public GameObject cardPrefab;
    public bool isCrystalSlot = false;


    // Start is called before the first frame update
    void Start()
    {
        _outline = GetComponent<Outline>();
        if (isCrystalSlot){
            PlaceCard(CardDatabase.Instance.cardService.GetCardFromId(0));
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.name.Contains("CardVisualHand")){
            if (other.transform.GetComponent<CardHandController>().isDragging)
                _outline.enabled = true;
        } 
    }

    void OnTriggerExit2D(Collider2D other){
        _outline.enabled = false;
    }

    public void PlaceCard(Card card){
        //creates CardContainer object
        GameObject container = new GameObject("CardContainer");
        container.AddComponent<RectTransform>();
        container.transform.SetParent(transform, false);
        container.transform.localPosition = Vector3.zero;
        container.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 60);
        HorizontalLayoutGroup hzl = container.AddComponent<HorizontalLayoutGroup>();
        hzl.childControlHeight = true;
        hzl.childControlWidth = true;

        //instantiate object and places it in the new CardContainer
        GameObject cv = Instantiate(cardPrefab, container.transform);
        cv.transform.localScale = new Vector3(0.17f, 0.17f);
        container.transform.localPosition = Vector3.back;
        cv.GetComponent<DisplayCard>().LoadCard(card);
    }
}
