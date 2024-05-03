using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneHelper : MonoBehaviour
{
    [Tooltip("The dropdown box with runes")]
    public TMP_Dropdown dropdown;

    [Tooltip("Runes present in the game")]
    public Sprite[] runes;

    void Start()
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> runeitems = new List<TMP_Dropdown.OptionData>();

        foreach(var rune in runes)
        {
            var runeOption = new TMP_Dropdown.OptionData(rune.name, rune);
            runeitems.Add(runeOption);
        }

        dropdown.AddOptions(runeitems);
    }
}
