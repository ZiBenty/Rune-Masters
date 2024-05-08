using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour
{
    public Card Card;
    public TMP_Text NameText;
    public TMP_Text EffectText;
    public Image RuneImage;
    public Image TypeImage;
    public Image Art;
    public TMP_Text StarsText;
    public TMP_Text AttackText;
    public TMP_Text LifeText;
    public List<Image> CostImages;
    /*
    0=top-left
    1=top
    2=top-right
    3=bottom-left
    4=bottom
    5=bottom-right
    */

    public List<Sprite> RuneSprites;
    /*
    0=None
    1=Fire
    2=Earth
    3=Air
    4=Water
    5=Ancestral
    */
    public List<Sprite> TypeSprites;
    /*
    0=None
    1=Creature
    2=Structure
    3=Enchantment
    */
    public void changeName(string name){
        NameText.text = name;
    }

    public void changeRune(Sprite sprite, Rune rune){
        switch(rune){
            case Rune.Fire:
                sprite = RuneSprites[1];
                break;
            case Rune.Earth:
                sprite = RuneSprites[2];
                break;
            case Rune.Air:
                sprite = RuneSprites[3];
                break;
            case Rune.Water:
                sprite = RuneSprites[4];
                break;
            case Rune.Ancestral:
                sprite = RuneSprites[5];
                break;
            default:
                sprite = RuneSprites[0];
                break;
        }
    }

    public void changeType(CardType type){
        switch(type){
            case CardType.Creature:
                TypeImage.sprite = TypeSprites[1];
                break;
            case CardType.Structure:
                TypeImage.sprite = TypeSprites[2];
                break;
            case CardType.Enchantment:
                TypeImage.sprite = TypeSprites[3];
                break;
            default:
                TypeImage.sprite = TypeSprites[0];
                break;
        }
    }

    public void changeStars(int stars){
        if(stars > 0 && stars <=3){
            switch(stars){
                case 1:
                    StarsText.text = "*";
                    break;
                case 2:
                    StarsText.text = "**";
                    break;
                case 3:
                    StarsText.text = "***";
                    break;
            }
        }else if (stars>3){
            StarsText.text = "***";
        }else{
            StarsText.text = "";
        }
    }

    public void changeEffect(string effect){
        EffectText.text = effect;
    }

    public void changeAttack(int attack){
        AttackText.text = attack.ToString();
    }

    public void changeLife(int life){
        LifeText.text = life.ToString();
    }

    public void changeCost(List<Rune> cost){
        changeRune(CostImages[0].sprite, cost[0]);
        changeRune(CostImages[1].sprite, cost[1]);
        changeRune(CostImages[2].sprite, cost[2]);
        changeRune(CostImages[3].sprite, cost[3]);
        changeRune(CostImages[4].sprite, cost[4]);
        changeRune(CostImages[5].sprite, cost[5]);
    }

    public void LoadCard (Card c){
        Card = c;
        changeName(c.Name);
        changeRune(RuneImage.sprite, c.CardRune);
        changeType(c.CardType);
        changeStars(c.Stars);
        changeEffect(c.Effect);
        changeAttack(c.Atk);
        changeLife(c.Hp);
        changeCost(c.decodeCost());
    }

    public void Start(){

    }

}
