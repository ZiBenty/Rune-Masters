using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuneHelper : MonoBehaviour
{
    [Tooltip("The dropdown box with runes")]
    public Dropdown dropdown;

    [Tooltip("Runes present in the game")]
    public Sprite[] runes;

    void Start()
    {
        dropdown.ClearOptions();

        List<Dropdown.OptionData> runeitems = new List<Dropdown.OptionData>();

        foreach(var rune in runes)
        {
            var runeOption = new Dropdown.OptionData(rune.name, rune);
            runeitems.Add(runeOption);
        }

        dropdown.AddOptions(runeitems);
    }
}
