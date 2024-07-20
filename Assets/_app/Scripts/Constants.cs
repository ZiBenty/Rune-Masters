using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Constants
{
    public static Color PlayerColor = new Color(17f, 195f, 0f, 255f);
    public static Color EnemyColor = new Color(195f, 33f, 0f, 255f);

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
        Discard = 8,
        Removed = 16,
        Inspected = 32
    }
}
