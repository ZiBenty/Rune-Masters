using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DBScene : MonoBehaviour
{
    CardService cardService;
    public TMP_InputField nameInput;
    public TMP_InputField effectInput;
    public TMP_Text runeDropdown;

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
        
        string cardName = nameInput.text.ToString();
        string cardEffect = effectInput.text.ToString();
        string rune = runeDropdown.text.ToString();

        int ck = cardService.AddCard(cardService.CreateCard(cardName, cardEffect, rune));

        Debug.Log("Primary Key = " + ck);
    }
    
}
