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

    public void changeName(TMP_InputField Input){
        Name.text = Input.text.ToString();
    }

    public void changeRune(TMP_Text Input){
        Rune.sprite = Resources.Load<Sprite>("Assets/_app/Sprites/" + Input.text.ToString());
    }

    public void changeType(TMP_Text Input){
        Type.sprite = Resources.Load<Sprite>("Assets/_app/Sprites/" + Input.text.ToString());
    }

    public void changeStars(TMP_InputField Input){
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

    public void changeEffect(TMP_InputField Input){
        Effect.text = Input.text.ToString();
    }

    public void changeAttack(TMP_InputField Input){
        Attack.text = Input.text.ToString();
    }

    public void changeLife(TMP_InputField Input){
        Life.text = Input.text.ToString();
    }

}


