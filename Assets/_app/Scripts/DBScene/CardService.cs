using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class CardService
{
    DB db;

    public CardService(){
        db = new DB();
    }

    public Rune decodeRuneString(string r)
    {
        switch (r){
            case "Water":
                return Rune.Water;
            case "Fire":
                return Rune.Fire;
            case "Earth":
                return Rune.Earth;
            case "Air":
                return Rune.Air;
            case "Ancestral":
                return Rune.Ancestral;
            default:
                return Rune.None;
        }
    }

    public CardType decodeCardTypeString(string r)
    {
        switch (r){
            case "Creature":
                return CardType.Creature;
            case "Structure":
                return CardType.Structure;
            case "Enchantment":
                return CardType.Enchantment;
            default:
                return CardType.None;
        }
    }

    public Rune decodeIntCost(int n)
    {
        switch(n){
            case 0:
                return Rune.None;
            case 1:
                return Rune.Fire;
            case 2:
                return Rune.Earth;
            case 3:
                return Rune.Air;
            case 4:
                return Rune.Water;
            default:
                return Rune.None;
        }
    }

    public void CreateCardTableDB(){
        db.GetConnection().DropTable<Card> ();
        db.GetConnection().CreateTable<Card> ();
    }

    public IEnumerable<Card> GetCards(){
		return db.GetConnection().Table<Card>();
	}

    public IEnumerable<Card> GetCardsFromName(string name){
		return db.GetConnection().Table<Card>().Where(c => c.Name.Contains(name));
	}

    public IEnumerable<Card> GetCardsFromRune(Rune rune){
        return db.GetConnection().Table<Card>().Where(c => c.CardRune == rune);
    }

    public IEnumerable<Card> GetCardsFromRune(CardType type){
        return db.GetConnection().Table<Card>().Where(c => c.CardType == type);
    }

    public IEnumerable<Card> GetCardsFromAttack(int atk){
        return db.GetConnection().Table<Card>().Where(c => c.Atk == atk);
    }

    public IEnumerable<Card> GetCardsFromLife(int hp){
        return db.GetConnection().Table<Card>().Where(c => c.Hp == hp);
    }

    public IEnumerable<Card> GetCardsFromStars(int stars){
        return db.GetConnection().Table<Card>().Where(c => c.Stars == stars);
    }

    public Card GetCardFromId(int id){
		return db.GetConnection().Table<Card>().Where(c => c.Id == id).FirstOrDefault();
	}

    public int AddCard(Card card){
        return db.GetConnection().Insert(card);
    }

    public int UpdateCard(Card card){
        return db.GetConnection().Update(card);
    }

    public Card CreateCard(List<int> cost,
                          string name = "", 
                          string effect = "", 
                          string rune = "", 
                          string type = "",
                          int atk = 0,
                          int hp = 0,
                          int stars = 1)
    {
        var c = new Card{
            Name = name,
            Effect = effect,
            CardRune = decodeRuneString(rune),
            CardType = decodeCardTypeString(type),
            Atk = atk,
            Hp = hp,
            Stars = stars,
        };
        List<Rune> l = new List<Rune>();
        foreach (int symb in cost){
            l.Add(decodeIntCost(symb));
        }
        c.Cost = c.EncodeCost(l);
        return c;
    }
}
