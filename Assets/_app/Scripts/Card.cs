using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using Unity.Collections;


public class Card {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
    public string Effect {get; set;}
	public Rune CardRune {get; set; }
	public Type CardType { get; set; }
    public int Atk { get; set; }
    public int Hp { get; set; }
    public int Stars { get; set; }
    public int Cost { get; set; }
    /* cost is a list of runes later translated in a 32 bit number in db
    pos 0 = top-left
    pos 1 = top
    pos 2 = top-right
    pos 3 = bottom-left
    pos 4 = bottom
    pos 5 = bottom-right*/

    public enum Rune 
    {
        None = 0,
        Fire = 1,
        Earth = 2,
        Air = 4,
        Water = 8,
        Ancestral = 16
    }

    public enum Type
    {
        Creature = 1,
        Structure = 2,
        Enchantment = 4
    }

    public List<Rune> decodeCost()
    {
        List<Rune> l = new List<Rune>();
        return l;
    }

    public void encodeCost(List<Rune> l)
    {

    }

	public override string ToString ()
	{
        var str1 = string.Format ("Id={0},\n Name={1},\n  Rune={2},\n Type={3},\n Atk={4},\n Hp={5},\n Stars={6}\n",
                                 Id, Name, CardRune, CardType, Atk, Hp, Stars);
        var CostList = decodeCost();
        var str2 = string.Format("Cost:\n {0} | {1} | {2} \n {3} | {4} | {5} \n", CostList[0], CostList[1], CostList[2], CostList[3], CostList[4], CostList[5]);
		return str1+str2;
	}
}

        