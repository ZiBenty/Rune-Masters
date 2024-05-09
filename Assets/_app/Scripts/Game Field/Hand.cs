using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;

    public List<GameObject> hand;
    //public CardDatabase db;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
        int i = 0;
        foreach(Transform child in transform){
            child.gameObject.GetComponent<DisplayCard>().LoadCard(db.Cards[i]);
            i++;
        }
        */
    }

    public void AddCard(GameObject card){
        hand.Add(card);
    }
}
