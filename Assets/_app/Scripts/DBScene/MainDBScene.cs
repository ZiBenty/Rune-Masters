using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class DBScene : MonoBehaviour
{
    CardService cardService;
    public TMP_InputField nameInput;
    public TMP_InputField effectInput;
    public TMP_Text runeDropdown;
    public TMP_Text typeDropdown;
    public TMP_InputField atkInput;
    public TMP_InputField hpInput;
    public TMP_InputField starsInput;
    public CostHelper costHelper;

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
        string type = typeDropdown.text.ToString();
        int atk, hp, stars;
        try
        {
            atk = int.Parse(atkInput.text.ToString());
            hp = int.Parse(hpInput.text.ToString());
            stars = int.Parse(starsInput.text.ToString());
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            atk = 0;
            hp = 0;
            stars = 1;
        }

        int ck = cardService.AddCard(cardService.CreateCard(costHelper.CostCodeIndex, cardName, cardEffect, rune, type, atk, hp, stars));
        Debug.Log("Primary Key = " + ck);
    }
    
}
