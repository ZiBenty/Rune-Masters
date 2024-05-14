using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;

    public List<Card> hand;

    private int lastHandSize = 0;
    
    public List<GameObject> handVisual;

    // Start is called before the first frame update
    void Start()
    {
        hand = new List<Card>();
        handVisual = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        lastHandSize = hand.Count;
        if (lastHandSize != handVisual.Count){
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
            GameObject cv = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            cv.transform.SetParent(transform);
            cv.GetComponent<DisplayCard>().LoadCard(card);
            cv.transform.localPosition = new Vector3(0,0,10);
            cv.transform.eulerAngles = new Vector3(20, 0, 0);
            handVisual.Add(cv); 
        }
        
        
    }
}
