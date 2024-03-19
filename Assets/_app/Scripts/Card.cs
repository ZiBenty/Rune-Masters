using System.Collections.Generic;
using SQLite4Unity3d;
using Unity.Collections;


public class Person {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
	public Rune CardRune {get; set; }
	public Type CardType { get; set; }
    public int Atk { get; set; }
    public int Hp { get; set; }
    public int Stars { get; set; }
    public List<Rune> Cost { get; set; }
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

	public override string ToString ()
	{
        var str1 = string.Format ("Id={0},\n Name={1},\n  Rune={2},\n Type={3},\n Atk={4},\n Hp={5},\n Stars={6}\n",
                                 Id, Name, CardRune, CardType, Atk, Hp, Stars);
        var str2 = string.Format("Cost:\n {0} | {1} | {2} \n {3} | {4} | {5} \n", Cost[0], Cost[1], Cost[2], Cost[3], Cost[4], Cost[5]);
		return str1+str2;
	}
}

        