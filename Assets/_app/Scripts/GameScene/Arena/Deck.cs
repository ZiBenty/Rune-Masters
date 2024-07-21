using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Constants; 

public class Deck : MonoBehaviour
{
    [SerializeField]
    private GameObject _cardPrefab;
    private Image _visual;
    public List<GameObject> DeckList;
    private int _oldDeckCount;
    public Player Owner;

    // Start is called before the first frame update
    void Start()
    {
        _visual = GetComponent<Image>();
        DeckList = new List<GameObject>();
        // TODO: implement import from decklist
        LoadDecklist("StarterFireAir");
        Shuffle();
    }

    void Update()
    {
        if (DeckList.Count == 0)
            _visual.enabled = false;
        if (_oldDeckCount == 0 && DeckList.Count > 0)
            _visual.enabled = true;
        if (_oldDeckCount != DeckList.Count)
            _oldDeckCount = DeckList.Count;
    }

    public void AddCard(GameObject card){
        GameObject copy = Instantiate(card, transform);
        copy.GetComponent<CardInfo>().LoadInfo(card.GetComponent<CardInfo>().BaseInfo);
        if (copy.transform.childCount != 0)
            Destroy(copy.transform.GetChild(0).gameObject); // removes visual from copy object
        copy.GetComponent<CardState>().Location = Constants.Location.Deck;
        DeckList.Add(copy);
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
                    if (card.GetComponent<CardInfo>().TempInfo.CardType == CardType.Creature)
                        card.AddComponent<MoveComponent>();
                    card.GetComponent<CardState>().SetPlayer(Owner);
                    card.GetComponent<CardState>().Location = Constants.Location.Deck;
                    DeckList.Add(card);
                }
            }
        }while(line !=null);
        _oldDeckCount = DeckList.Count;
    }

    public void Shuffle(){
        DeckList = DeckList.OrderBy(_ => Guid.NewGuid()).ToList();
    }


}
