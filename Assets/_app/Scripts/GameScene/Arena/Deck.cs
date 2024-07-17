using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;
    private Image _visual;
    public List<GameObject> DeckList;
    public Player Owner;

    // Start is called before the first frame update
    void Start()
    {
        _visual = GetComponent<Image>();
        DeckList = new List<GameObject>();
        if(transform.name == "PlayerDeck")
            Owner = GameManager.Instance.player;
        else
            Owner = GameManager.Instance.enemy;
        // TODO: implement import from decklist
        LoadDecklist("StarterFireAir");
        Shuffle();
    }

    void Update()
    {
        /*if (DeckList.Count == 0)
        {
            _visual.enabled = false;
        }*/
    }

    public void LoadDecklist(string name){
        StreamReader reader = new StreamReader("Assets/Resources/Decks/"+ name +".dck", true);
        string line;
        do{
            line = reader.ReadLine();
            if(line != null){
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < int.Parse(words[1]); i++){
                    Card c = CardDatabase.Instance.cardService.GetCardFromId(int.Parse(words[0]));
                    GameObject card = Instantiate(_cardPrefab, transform);
                    card.GetComponent<CardInfo>().LoadInfo(c);
                    card.GetComponent<CardState>().SetPlayer(Owner);
                    card.GetComponent<CardState>().Location = Constants.Location.Deck;
                    DeckList.Add(card);
                }
            }
        }while(line !=null);
    }

    public void Shuffle(){
        DeckList = DeckList.OrderBy(_ => Guid.NewGuid()).ToList();
    }


}
