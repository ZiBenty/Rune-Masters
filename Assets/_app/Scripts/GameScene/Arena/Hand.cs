using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Hand : MonoBehaviour
{
    public GameObject visualPrefab;
    private HorizontalLayoutGroup _horizLayoutGroup;
    //public List<Card> hand;
    private int _lastHandSize = 0;
    [SerializeField]
    private float DefaultSpacing = 129, OffsetSpacing = 20;


    // Start is called before the first frame update
    void Start()
    {
        _horizLayoutGroup = transform.GetComponent<HorizontalLayoutGroup>();
        //hand = new List<Card>();
    }

    // Update is called once per frame
    void Update()
    {
        //_lastHandSize = hand.Count;
        if (_lastHandSize != transform.childCount){
            ArrangeHand();
        }
    }

    public void AddCard(GameObject card){
        //hand.Add(card);
        ArrangeHand(card);
    }

    public void ArrangeHand(GameObject card = null){
        //Vector3 position;
        if (card != null){
            //creates CardContainer object
            GameObject container = new GameObject("CardContainer");
            container.AddComponent<RectTransform>();
            container.transform.SetParent(transform, false);

            //instantiate object and places it in the new CardContainer
            GameObject cv = Instantiate(visualPrefab, card.transform);
            cv.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            cv.GetComponent<CardDisplay>().LoadCard();

            card.transform.SetParent(container.transform, false);
            card.GetComponent<CardState>().Location = Constants.Location.Hand;
        }else{
            for(int i = 0; i < transform.childCount; i++){
                if(transform.GetChild(i).childCount == 0)
                    Destroy(transform.GetChild(i));
                    break;
            }
        }
        if (transform.childCount > 7 && transform.childCount > _lastHandSize){
            _horizLayoutGroup.spacing = _horizLayoutGroup.spacing - OffsetSpacing*(transform.childCount-_lastHandSize);
        }else if (transform.childCount > 7 && transform.childCount < 15){
            _horizLayoutGroup.spacing = _horizLayoutGroup.spacing + OffsetSpacing*(_lastHandSize-transform.childCount);
        }
        else{
            _horizLayoutGroup.spacing = DefaultSpacing;
        }
        _lastHandSize = transform.childCount;
    }

    public void setDraggable(bool draggable){
        for(int i = 0; i < transform.childCount; i++){
                transform.GetChild(i).GetChild(0).GetComponent<PlayScript>().SetcanDrag(draggable);
            }
    }
}
