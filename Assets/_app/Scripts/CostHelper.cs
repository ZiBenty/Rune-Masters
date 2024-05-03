using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public List<string> CostCode = new List<string>
    {
        "None", "Fire", "Earth", "Air", "Water"
    };
    /*
    0 = None
    1 = Fire
    2 = Earth
    3 = Air
    4 = Water
    */

    public List<int> CostCodeIndex = new List<int>(6)
    {
        0, 0, 0, 0, 0, 0
    };

    public void changeRune()
    {

    }

}
