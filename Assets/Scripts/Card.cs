using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Collections;
using UnityEngine;

//use this if number of cards < 500, use sqlite3 instead if > 500
namespace FrancisProductions
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")] //can be created in unity menu
    public class Card : ScriptableObject
    {
        public int id;
        public string cardName;
        public CardRune cardRune;
        public CardType cardType;
        public int atk;
        public int hp;
        public int stars; //limits the number of cards on the field
        [HideInInspector]
        private int width = 3, height = 2;
        public CardRune[] costUpper; //upper line
        public CardRune[] costLower; //lower line

        public enum CardRune
        {
            None,
            Fire,
            Earth,
            Air,
            Water,
            Ancestral
        }

        public enum CardType
        {
            Creature,
            Structure,
            Enchantment
        }

    }
}