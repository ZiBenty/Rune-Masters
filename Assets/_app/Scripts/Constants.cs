using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public enum Rune 
    {
        None = 0,
        Fire = 1,
        Earth = 2,
        Air = 4,
        Water = 8,
        Ancestral = 15
    }

    public enum CardType
    {
        None = 0,
        Creature = 1,
        Structure = 2,
        Enchantment = 4
    }

    public enum Location
    {
        None = 0,
        Deck = 1,
        Hand = 2,
        Field = 4, 
        Grave = 8,
        Removed = 16,
        Inspected = 32
    }
}
