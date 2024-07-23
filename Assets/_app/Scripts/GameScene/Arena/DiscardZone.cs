using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscardZone : MonoBehaviour, IInspect
{
    [SerializeField]
    public GameObject visualPrefab;
    private int _lastPileSize = 0;
    private GameObject lastAddedCard;
    public Player Owner;
    [SerializeField]
    private GameObject InspectDiscardZone;

    [Header("Inspect")]
    private bool _canInspect = true;

    public void SetcanInspect(bool b){
        _canInspect = b;
    }
    public bool GetcanInspect(){
        return _canInspect;
    }
    public void onStartInspect()
    {
        //se è aperto l'altro, prima chiudilo
        if (InspectDiscardZone.activeSelf){
            CloseInspectDiscardZone();
        }
        
        if (Owner.transform.name == "Player")
            InspectDiscardZone.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Player's Discard Zone";
        else
            InspectDiscardZone.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Enemy's Discard Zone";
        FillInspectDiscardZone();
        InspectDiscardZone.SetActive(true);
        GameObject.Find("Player").GetComponent<Player>().SetCardsOnFieldInspectable(false);
        GameObject.Find("Enemy").GetComponent<Player>().SetCardsOnFieldInspectable(false);
        Arena arena = GameObject.Find("Arena").GetComponent<Arena>();
        arena.SetSlotsInspectable(false);
        arena.GetComponent<CanvasGroup>().blocksRaycasts = false;
        arena.GetComponent<CanvasGroup>().interactable = false;
    }

    public void onStopInspect()
    {

    }

    public void CloseInspectDiscardZone(){
        if (InspectDiscardZone.activeSelf){
            DefillInspectDiscardZone();
            InspectDiscardZone.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().SetCardsOnFieldInspectable(true);
            GameObject.Find("Enemy").GetComponent<Player>().SetCardsOnFieldInspectable(true);
            Arena arena = GameObject.Find("Arena").GetComponent<Arena>();
            arena.SetSlotsInspectable(true);
            arena.GetComponent<CanvasGroup>().blocksRaycasts = true;
            arena.GetComponent<CanvasGroup>().interactable = true;
        }
    }

    private void FillInspectDiscardZone(){
        if (transform.GetChild(0).childCount != 0){
            for(int i = 0; i < transform.GetChild(0).childCount; i++){
                GameObject copy = Instantiate(transform.GetChild(0).GetChild(i).gameObject);
                copy.GetComponent<CardInfo>().LoadInfo(transform.GetChild(0).GetChild(i).GetComponent<CardInfo>().BaseInfo);

                GameObject container = new GameObject("CardContainer");
                container.AddComponent<RectTransform>();
                container.transform.SetParent(InspectDiscardZone.transform.GetChild(1), false);

                copy.transform.SetParent(container.transform, false);
                copy.GetComponent<PlayScript>().SetcanInspect(true);
                Vector3 newScale = new Vector3(0.08f, 0.08f, 0.08f);
                copy.transform.GetChild(0).transform.localScale = newScale;
                if (Owner.transform.name == "Enemy")
                    copy.transform.eulerAngles = new Vector3(0, 0, 0);
                copy.GetComponent<BoxCollider2D>().enabled = true;
                copy.GetComponent<BoxCollider2D>().size = copy.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta*newScale;
                copy.transform.GetChild(0).gameObject.SetActive(true);
                
            }
        }
    }

    private void DefillInspectDiscardZone(){
        foreach(Transform child in InspectDiscardZone.transform.GetChild(1)){
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (_lastPileSize != transform.GetChild(0).childCount)
            ShowLastCard();
        if (Owner.deckScript.DeckList.Count == 0 && transform.GetChild(0).childCount>0){ //empties discard zone and shuffles it into deck
            foreach(Transform card in transform.GetChild(0).transform){
                StartCoroutine(GameManager.Instance.MoveLocation(card.gameObject, Constants.Location.Deck));
            }
            foreach(Transform card in transform.GetChild(0).transform){
                Destroy(card.gameObject);
            }
            lastAddedCard = null;
            Owner.deckScript.Shuffle();
        }
        
    }

    public void AddCard(GameObject card){
        GameObject copy = Instantiate(card);
        copy.GetComponent<CardInfo>().LoadInfo(card.GetComponent<CardInfo>().BaseInfo);
        if (copy.transform.childCount != 0)
            Destroy(copy.transform.GetChild(0).gameObject); // removes visual from copy object
        
        //adds the card to the discrad pile
        copy.transform.SetParent(transform.GetChild(0), false);
        lastAddedCard = copy;
        
        GameObject cv = Instantiate(visualPrefab, copy.transform);
        cv.transform.localScale = new Vector3(0.20f, 0.20f);
        cv.GetComponent<CardDisplay>().LoadCard();
        copy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        copy.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        copy.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);

        copy.GetComponent<CardState>().Location = Constants.Location.Discard; 
    }

    public void ShowLastCard(){
        if(transform.GetChild(0).childCount != 0){
            for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(0).GetChild(i).transform.GetChild(0).gameObject.SetActive(false); 
            lastAddedCard.transform.GetChild(0).gameObject.SetActive(true);
        }
        _lastPileSize = transform.childCount;
    }
    
}
