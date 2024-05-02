using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardService
{
    DB db;

    public CardService(){
        db = new DB();
    }

    public void CreateCardTableDB(){
      db.GetConnection().DropTable<Card> ();
      db.GetConnection().CreateTable<Card> ();
    }

    public int AddCard(Card card){
      return db.GetConnection().Insert(card);
    }

    public Card CreateCard(string name = "", string effect = ""){
      var c = new Card{
          Name = name,
          Effect = effect,
          CardRune = Rune.Fire,
          CardType = Type.Creature,
          Atk = 2,
          Hp = 1,
          Stars = 3,
      };
      List<Rune> l = new List<Rune>(){Rune.None, Rune.None, Rune.None, Rune.None, Rune.Fire, Rune.None};
      c.Cost = c.encodeCost(l);
      return c;
    }
}
