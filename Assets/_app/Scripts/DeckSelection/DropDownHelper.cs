using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DropDownHelper : MonoBehaviour
{
    public TMP_Dropdown PlayerDeck;
    public TMP_Dropdown EnemyDeck;

    private bool done = false;

    private void StartSettings(){
        if (Settings.Instance == null){
            GameObject settings = new GameObject("Settings");
            settings.AddComponent<Settings>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSettings();
        
    }

    void Update(){
        if (Settings.Instance.Ready && !done){
            PlayerDeck.ClearOptions();
            EnemyDeck.ClearOptions();

            List<TMP_Dropdown.OptionData> decks = new List<TMP_Dropdown.OptionData>();
            
            DirectoryInfo d = new DirectoryInfo(Settings.Instance.decksPath);
            foreach (var file in d.GetFiles())
            {
                if (!file.Name.Contains(".meta")){
                    var deckName = file.Name.Replace(".dck", "");
                    deckName = Regex.Replace(deckName, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
                    var deckOption = new TMP_Dropdown.OptionData(deckName);
                    decks.Add(deckOption);
                }
            }

            PlayerDeck.AddOptions(decks);
            EnemyDeck.AddOptions(decks);
            done = !done;
        }
    }

}
