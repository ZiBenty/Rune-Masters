using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script di prova, cancellare dopo

public class Hand : MonoBehaviour
{
    public CardDatabase db;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(Transform child in transform){
            child.gameObject.GetComponent<DisplayCard>().LoadCard(db.cards.ElementAt<Card>(i));
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
