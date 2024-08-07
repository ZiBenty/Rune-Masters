using System;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using Unity.Collections;
using UnityEngine;
using static Constants;

public class Card {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
    public string Effect {get; set;}
	public Rune CardRune {get; set; }
	public CardType CardType { get; set; }
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

    public List<Rune> DecodeCost()
    {
        List<Rune> l = new List<Rune>();
        string binary = Convert.ToString(Cost, 2);
        string substring = "";

        //every Rune is 4 bit
        int jump;
        if (binary.Length % 4 == 0)
            jump = 4;
        else
            jump = binary.Length % 4;
        
        //if binary is less than 24 it could mean a bunch of None Runes int the top rows
        for (int i = 20; i >= binary.Length; i -= 4)
        {
            l.Add(Rune.None);
        }

        for (int i = 0; i < binary.Length; i += jump)
        {
            if (i != 0 && jump != 4)
                jump = 4;
            substring = binary.Substring(i, Math.Min(jump, binary.Length - i));
            int number = Convert.ToInt32(substring, 2);
            switch (number){
                case 0:
                    l.Add(Rune.None);
                    break;
                case 1:
                    l.Add(Rune.Fire);
                    break;
                case 2:
                    l.Add(Rune.Earth);
                    break;
                case 4:
                    l.Add(Rune.Air);
                    break;
                case 8:
                    l.Add(Rune.Water);
                    break;
            }
        }
        return l;
    }

    public int EncodeCost(List<Rune> l)
    {
        var cost = 0;
        cost = cost | (int)l[0] << 20 | (int)l[1] << 16 | (int)l[2] << 12  | (int)l[3] << 8 | (int)l[4] << 4 | (int)l[5];
        return cost;
    }

	public override string ToString ()
	{
        var str1 = string.Format ("Id={0},\n Name={1},\n  Rune={2},\n Type={3},\n Atk={4},\n Hp={5},\n Stars={6}\n",
                                 Id, Name, CardRune, CardType, Atk, Hp, Stars);
        var CostList = DecodeCost();
        var str2 = string.Format("Cost:\n {0} | {1} | {2} \n {3} | {4} | {5} \n", CostList[0], CostList[1], CostList[2], CostList[3], CostList[4], CostList[5]);
		return str1+str2;
	}
}

        