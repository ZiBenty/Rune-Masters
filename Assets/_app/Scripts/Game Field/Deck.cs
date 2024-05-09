using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardDatabase db;

    public List<GameObject> deck;
    // Start is called before the first frame update
    void Start()
    {
        deck = new List<GameObject>();
        for(int i=0;i<30;i++)
        {
            deck.Add(Instantiate(cardPrefab, this.transform));
            deck.ElementAt<GameObject>(i).GetComponent<DisplayCard>().CardBack = true;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
