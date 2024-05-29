using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    private HorizontalLayoutGroup _horizLayoutGroup;
    public List<Card> hand;
    public List<GameObject> handVisual;
    private int _lastHandSize = 0;
    [SerializeField]
    private float DefaultSpacing = 129, OffsetSpacing = 20;


    // Start is called before the first frame update
    void Start()
    {
        _horizLayoutGroup = transform.GetComponent<HorizontalLayoutGroup>();
        hand = new List<Card>();
        handVisual = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        _lastHandSize = hand.Count;
        if (_lastHandSize != handVisual.Count){
            ArrangeHand();
        }
    }

    public void AddCard(Card card){
        hand.Add(card);
        ArrangeHand(card);
    }

    public void ArrangeHand(Card card = null){
        //Vector3 position;
        if (card != null){
            //creates CardContainer object
            GameObject container = new GameObject("CardContainer");
            container.AddComponent<RectTransform>();
            container.transform.SetParent(transform, false);

            //instantiate object and places it in the new CardContainer
            GameObject cv = Instantiate(cardPrefab, container.transform);
            cv.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);

            cv.GetComponent<DisplayCard>().LoadCard(card);
            handVisual.Add(cv);  
        }else{
            for(int i = 0; i < transform.childCount; i++){
                if(transform.GetChild(i).childCount == 0)
                    Destroy(transform.GetChild(i));
                    break;
            }
        }
        if (handVisual.Count > 7 && handVisual.Count > _lastHandSize){
            _horizLayoutGroup.spacing = _horizLayoutGroup.spacing - OffsetSpacing*(handVisual.Count-_lastHandSize);
        }else if (handVisual.Count > 7 && handVisual.Count < 15){
            _horizLayoutGroup.spacing = _horizLayoutGroup.spacing + OffsetSpacing*(_lastHandSize-handVisual.Count);
        }
        else{
            _horizLayoutGroup.spacing = DefaultSpacing;
        }
    }
}
