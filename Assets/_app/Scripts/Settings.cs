using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance {get; private set;}

    public string decksPath = "";

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){

        if (!Directory.Exists(Application.persistentDataPath))
            File.Create(Application.persistentDataPath);
        
        #if UNITY_EDITOR
            decksPath = @"Assets/StreamingAssets/Decks/";
        #else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "Decks/");

        if (!Directory.Exists(filepath))
        {
            string path = Path.Combine(Application.persistentDataPath, "Decks");
            Directory.CreateDirectory(path);
            
            #if UNITY_ANDROID 
            var loadDecks = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "Decks/");  // this is the path to your StreamingAssets in android
            while (!loadDecks.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            
            File.WriteAllBytes(filepath, loadDecks.bytes);

            #elif UNITY_IOS
                 var loadDecks = Application.dataPath + "/Raw/" + "Decks/";  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDecks, filepath);
            #endif
        }

        var decksPath = filepath;
        #endif
    }

}
