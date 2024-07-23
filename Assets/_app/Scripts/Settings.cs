using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance {get; private set;}

    public string decksPath = "";

    public bool Ready = false;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        
        #if UNITY_EDITOR
            decksPath = @"Assets/StreamingAssets/decks";
        #else
        if (!Directory.Exists(Application.persistentDataPath))
            File.Create(Application.persistentDataPath);
        // check if directory exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, "decks");

        if (!Directory.Exists(filepath))
        {
            //create directory
            string path = Path.Combine(Application.persistentDataPath, "decks");
            Directory.CreateDirectory(path);

            #if UNITY_ANDROID 
            //saves both starter decks
            var loadDecks = new WWW("jar:file://" + Application.dataPath + "!/assets/decks/StarterFireAir.dck");  // this is the path to your StreamingAssets in android
            while (!loadDecks.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDecks.bytes);

            loadDecks = new WWW("jar:file://" + Application.dataPath + "!/assets/decks/StarterWaterEarth.dck");  // this is the path to your StreamingAssets in android
            while (!loadDecks.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDecks.bytes);

            #elif UNITY_IOS
            //saves both starter decks
            var loadDecks = Application.dataPath + "/Raw/decks/StarterFireAir.dck";  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Copy(loadDecks, filepath);

            loadDecks = Application.dataPath + "/Raw/decks/StarterWaterEarth.dck";  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Copy(loadDecks, filepath);
            #endif
        }

        decksPath = filepath;
        #endif
        Ready = true;
    }
    
}
