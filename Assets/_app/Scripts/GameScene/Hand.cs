using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    private HorizontalLayoutGroup horizLayoutGroup;
    public List<Card> hand;
    public List<GameObject> handVisual;
    private int lastHandSize = 0;
    [SerializeField]
    public float defaultSpacing = 129, offsetSpacing = 20;


    // Start is called before the first frame update
    void Start()
    {
        horizLayoutGroup = transform.GetComponent<HorizontalLayoutGroup>();
        hand = new List<Card>();
        handVisual = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        lastHandSize = hand.Count;
        /*if (lastHandSize != handVisual.Count){
            ArrangeHand();
        }*/
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
            GameObject cv = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            cv.transform.SetParent(container.transform, false);
            cv.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);

            cv.GetComponent<DisplayCard>().LoadCard(card);
            handVisual.Add(cv); 
            //cv.transform.localPosition = new Vector3(0,0,10);
            //cv.transform.eulerAngles = new Vector3(20, 0, 0);
            if (handVisual.Count > 5 && handVisual.Count > lastHandSize){
                horizLayoutGroup.spacing = horizLayoutGroup.spacing - offsetSpacing*(handVisual.Count-lastHandSize);
            }else if (handVisual.Count > 5 && handVisual.Count > lastHandSize){
                horizLayoutGroup.spacing = horizLayoutGroup.spacing + offsetSpacing*(lastHandSize-handVisual.Count);
            }
            else{
                horizLayoutGroup.spacing = defaultSpacing;
            }
            
        }
    }
}
