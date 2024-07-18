using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardZone : MonoBehaviour, IInspect
{
    [SerializeField]
    public GameObject visualPrefab;
    private int _lastPileSize = 0;
    private GameObject lastAddedCard;
    public Player Owner;

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
        
    }

    public void onStopInspect()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if(transform.name == "PlayerDiscardZone")
            Owner = GameManager.Instance.player;
        else
            Owner = GameManager.Instance.enemy;
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
