using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DBScene : MonoBehaviour
{
    CardService cardService;
    public TMPro.TMP_Text nameInput;
    public TMPro.TMP_Text effectInput;
    public 

    // Start is called before the first frame update
    void Start()
    {
        var cardService = new CardService();
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
        
        string cardName = nameInput.text.ToString();
        string cardEffect = effectInput.text.ToString();

        int ck = cardService.AddCard(cardService.CreateCard(cardName, cardEffect));

        Debug.Log("Primary Key = " + ck);
    }
    
}
