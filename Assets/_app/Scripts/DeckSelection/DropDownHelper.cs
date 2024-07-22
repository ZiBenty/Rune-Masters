using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownHelper : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown PlayerDeck;
    [SerializeField] private TMP_Dropdown EnemyDeck;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeck.ClearOptions();
        EnemyDeck.ClearOptions();

        List<TMP_Dropdown.OptionData> decks = new List<TMP_Dropdown.OptionData>();
        
        /*#if UNITY_EDITOR
            var decksPath = @"Assets/StreamingAssets/Decks/";
        #else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "Decks/");

        if (!File.Exists(filepath))
        {
            #if UNITY_ANDROID 
            var loadDecks = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDecks.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDecks.bytes);

            #elif UNITY_IOS
                 var loadDecks = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDecks, filepath);
            #endif
        }

        var decksPath = filepath;
        #endif
    }*/
    }

}
