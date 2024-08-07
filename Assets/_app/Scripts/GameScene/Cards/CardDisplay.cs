using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.VFX;
using static Constants;

public class CardDisplay : MonoBehaviour
{
    // references to UI elements
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

    //sprites used for runes
    public List<Sprite> RuneSprites;
    /*
    0=None
    1=Fire
    2=Earth
    3=Air
    4=Water
    5=Ancestral
    */

    //sprites used for card types
    public List<Sprite> TypeSprites;
    /*
    0=None
    1=Creature
    2=Structure
    3=Enchantment
    */

    //variables used for showing/hiding Card Back
    public bool CardBack;
    public static bool staticCardBack;

    //Change methods, these change the corresponding UI element
    public void ChangeName(string name){
        NameText.text = name;
    }

    public void ChangeRune(Image image, Rune rune){
        switch(rune){
            case Rune.Fire:
                image.sprite = RuneSprites[1];
                break;
            case Rune.Earth:
                image.sprite = RuneSprites[2];
                break;
            case Rune.Air:
                image.sprite = RuneSprites[3];
                break;
            case Rune.Water:
                image.sprite = RuneSprites[4];
                break;
            case Rune.Ancestral:
                image.sprite = RuneSprites[5];
                break;
            default:
                image.sprite = RuneSprites[0];
                break;
        }
    }

    public void ChangeType(CardType type){
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

    public void ChangeStars(int stars){
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

    public void ChangeEffect(string effect){
        EffectText.text = effect;
    }

    public void ChangeAttack(int attack){
        AttackText.text = attack.ToString();
    }

    public void ChangeLife(int life){
        LifeText.text = life.ToString();
    }

    public void ChangeCost(List<Rune> cost){
        ChangeRune(CostImages[0], cost[0]);
        ChangeRune(CostImages[1], cost[1]);
        ChangeRune(CostImages[2], cost[2]);
        ChangeRune(CostImages[3], cost[3]);
        ChangeRune(CostImages[4], cost[4]);
        ChangeRune(CostImages[5], cost[5]);
    }

    public void ChangeArt(int id){
        Art.sprite = Resources.Load<Sprite>("Sprites/Art/"+id.ToString());
    }


    //saves new Card reference and invokes Change methods
    public void LoadCard(){
        Card c = transform.parent.GetComponent<CardInfo>().TempInfo;

        ChangeName(c.Name);
        ChangeRune(RuneImage, c.CardRune);
        if (TypeImage != null){
            if (c.Id == 0){ //cristallo
                TypeImage.transform.parent.GetComponent<Behaviour>().enabled = false;
            }else{
                ChangeType(c.CardType);
            }
        }
        if (StarsText != null){
            if (c.Id == 0){ //cristallo
                StarsText.transform.GetComponent<Behaviour>().enabled = false;
            }else{
                ChangeStars(c.Stars);
            }
        }
        if (EffectText != null)
            ChangeEffect(c.Effect);
        if (AttackText != null && LifeText != null){
            if (c.CardType == CardType.Enchantment){
                AttackText.transform.parent.GetComponent<Behaviour>().enabled = false;
                LifeText.transform.parent.GetComponent<Behaviour>().enabled = false;
            }else if (c.Id == 0){ //cristallo
                AttackText.transform.parent.GetComponent<Behaviour>().enabled = false;
                ChangeLife(c.Hp);
            }else{
                ChangeAttack(c.Atk);
                ChangeLife(c.Hp);
            }
        }
        if (CostImages.Count != 0){
            if (c.Id == 0){ //cristallo
                CostImages[0].transform.parent.GetComponent<Behaviour>().enabled = false;
            }else{
                ChangeCost(c.DecodeCost());
            }
        }
        ChangeArt(c.Id);
    }

    public void Update(){
        staticCardBack = CardBack;
    }

}
