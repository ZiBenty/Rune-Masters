using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBScene : MonoBehaviour
{
    CardService cardService;
    // Start is called before the first frame update
    void Start()
    {
        cardService = new CardService();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCreateCardTableButtonClick(){
        Debug.Log("-------------------onCreateCardTableButtonClick-------------------------");

        cardService.CreateCardTableDB();
    }

    public void onAddCardButtonClick(){
        Debug.Log("-------------------onAddCardButtonClick-------------------------");
        
        int ck = cardService.AddCard(cardService.CreateCard());

        Debug.Log("Primary Key = " + ck);
    }
    
}
