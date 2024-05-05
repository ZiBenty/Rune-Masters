using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class CostHelper : MonoBehaviour
{
    [Tooltip("The images on cost buttons")]
    public List<Image> CostImage;
    /*
    pos 0 = top-left
    pos 1 = top
    pos 2 = top-right
    pos 3 = bottom-left
    pos 4 = bottom
    pos 5 = bottom-right*/

    [Tooltip("The sprites of the runes to show")]
    public List<Sprite> sprites;

    [Tooltip("list that keeps memorized what sprite is shown for each")]
    public List<int> CostCodeIndex = new List<int>(6)
    {
        0, 0, 0, 0, 0, 0
    };
        /*
    0 = None
    1 = Fire
    2 = Earth
    3 = Air
    4 = Water
    */

    public void changeRuneImage(int index){
        if (CostCodeIndex[index] != 4)
        {
            CostCodeIndex[index] += 1;
        }else
        {
            CostCodeIndex[index] = 0;
        }
        
        CostImage[index].sprite = sprites[CostCodeIndex[index]];
    }

    public void changeRune(GameObject button)
    {
        string image_name = button.name.ToString().Remove(0, "Btn-".Length);
        switch (image_name){
            case "Top-Left":
                changeRuneImage(0);
                break;
            case "Top":
                changeRuneImage(1);
                break;
            case "Top-Right":
                changeRuneImage(2);
                break;
            case "Bottom-Left":
                changeRuneImage(3);
                break;
            case "Bottom":
                changeRuneImage(4);
                break;
            case "Bottom-Right":
                changeRuneImage(5);
                break;
        }
    }

}
