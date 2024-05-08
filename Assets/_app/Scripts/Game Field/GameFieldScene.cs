using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldScene : MonoBehaviour
{
    CardService cardService;
    public CardDatabase cardDatabase;

    // Start is called before the first frame update
    void Start()
    {
        cardService = new CardService();
        getAllCards();

    }

    public void getAllCards(){
        cardDatabase.cards = cardService.GetCardsFromName("Salamandra");
    }


}
