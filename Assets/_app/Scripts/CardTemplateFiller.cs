using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardTemplateFiller : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Effect;
    public Image Rune;
    public Image Type;
    public TMP_Text Stars;
    public TMP_Text Attack;
    public TMP_Text Life;
    public List<Image> Cost;
    /*
    0=top-left
    1=top
    2=top-right
    3=bottom-left
    4=bottom
    5=bottom-right
    */
    public GameObject CostHelper;
    

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
    void Start(){
        Rune.sprite = RuneSprites[0];
        Type.sprite = TypeSprites[0];
    }

    public void changeName(TMP_InputField Input){
        Name.text = Input.text.ToString();
    }

    public void changeRune(TMP_Text Input){
        switch(Input.text.ToString()){
            case "Fire":
                Rune.sprite = RuneSprites[1];
                break;
            case "Earth":
                Rune.sprite = RuneSprites[2];
                break;
            case "Air":
                Rune.sprite = RuneSprites[3];
                break;
            case "Water":
                Rune.sprite = RuneSprites[4];
                break;
            case "Ancestral":
                Rune.sprite = RuneSprites[5];
                break;
            default:
                Rune.sprite = RuneSprites[0];
                break;
        }
    }

    public void changeType(TMP_Text Input){
        switch(Input.text.ToString()){
            case "Creature":
                Type.sprite = TypeSprites[1];
                break;
            case "Structure":
                Type.sprite = TypeSprites[2];
                break;
            case "Enchantment":
                Type.sprite = TypeSprites[3];
                break;
            default:
                Type.sprite = TypeSprites[0];
                break;
        }
    }

    public void changeStars(TMP_InputField Input){
        if(Input.text.ToString()!=""){
            int num = int.Parse(Input.text.ToString());
            if(num > 0 && num <=3){
                switch(num){
                    case 1:
                        Stars.text = "*";
                        break;
                    case 2:
                        Stars.text = "**";
                        break;
                    case 3:
                        Stars.text = "***";
                        break;
                }
            }else if (num>3){
                Stars.text = "***";
            }else{
                Stars.text = "";
            }
        }
    }

    public void changeEffect(TMP_InputField Input){
        Effect.text = Input.text.ToString();
    }

    public void changeAttack(TMP_InputField Input){
        Attack.text = Input.text.ToString();
    }

    public void changeLife(TMP_InputField Input){
        Life.text = Input.text.ToString();
    }

    public void changeCost(){
        List<int> CCI = CostHelper.GetComponent<CostHelper>().CostCodeIndex;
        Cost[0].sprite = RuneSprites[CCI[0]];
        Cost[1].sprite = RuneSprites[CCI[1]];
        Cost[2].sprite = RuneSprites[CCI[2]];
        Cost[3].sprite = RuneSprites[CCI[3]];
        Cost[4].sprite = RuneSprites[CCI[4]];
        Cost[5].sprite = RuneSprites[CCI[5]];
    }

}


