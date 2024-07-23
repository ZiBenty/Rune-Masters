using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Constants
{
    public static Color PlayerColor = new Color(17f, 195f, 0f, 255f);
    public static Color EnemyColor = new Color(195f, 33f, 0f, 255f);

    public const string FireAir = "1 3\n2 2\n3 1\n5 2\n11 3\n12 2\n13 1\n15 2";
    public const string WaterEarth = "6 3\n7 2\n8 1\n10 2\n16 3\n17 2\n18 1\n20 2";

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
