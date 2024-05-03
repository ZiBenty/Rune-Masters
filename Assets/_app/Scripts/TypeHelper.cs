using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeHelper : MonoBehaviour
{
    [Tooltip("The dropdown box with types")]
    public TMP_Dropdown dropdown;

    [Tooltip("Types present in the game")]
    public Sprite[] types;

    void Start()
    {
        dropdown.ClearOptions();

        List<TMP_Dropdown.OptionData> typeitems = new List<TMP_Dropdown.OptionData>();

        foreach(var type in types)
        {
            var typeOption = new TMP_Dropdown.OptionData(type.name, type);
            typeitems.Add(typeOption);
        }

        dropdown.AddOptions(typeitems);
    }
}
