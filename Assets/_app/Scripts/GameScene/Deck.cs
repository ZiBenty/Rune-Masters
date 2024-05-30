using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    private Image _visual;
    public List<Card> DeckList;

    // Start is called before the first frame update
    void Start()
    {
        _visual = GetComponent<Image>();
        DeckList = new List<Card>();
        // TODO: implement import from decklist
        LoadDecklist("StarterFireAir");
        Shuffle();
    }

    void Update()
    {
        if (DeckList.Count == 0)
        {
            _visual.enabled = false;
        }
    }

    public void LoadDecklist(string name){
        StreamReader reader = new StreamReader("Assets/Resources/Decks/"+ name +".dck", true);
        string line;
        do{
            line = reader.ReadLine();
            if(line != null){
                string[] words = line.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < int.Parse(words[1]); i++){
                    DeckList.Add(CardDatabase.Instance.cardService.GetCardFromId(int.Parse(words[0])));
                }
            }
        }while(line !=null);
    }

    public void Shuffle(){
        DeckList = DeckList.OrderBy(_ => Guid.NewGuid()).ToList();
    }


}
