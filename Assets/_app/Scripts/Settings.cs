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
        //GetDecksPath();
        Ready = true;
    }

    /*private void GetDecksPath(){
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

            var realPath = "";
            string oriPath;
            byte[] deck;

            #if UNITY_ANDROID

            oriPath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/decks/StarterFireAir.dck"); //returns a DirectoryInfo object
            UnityWebRequest reader = UnityWebRequest.Get(oriPath);
            reader.SendWebRequest();
            while (!reader.isDone) { }
            realPath = filepath + "/StarterFireAir.dck"; //save the file with this name
            File.WriteAllBytes(realPath, reader.downloadHandler.data);
            Debug.Log("StarterFireAir.dck saved in decks folder");

            oriPath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/decks/StarterWaterEarth.dck"); //returns a DirectoryInfo object
            UnityWebRequest reader = UnityWebRequest.Get(oriPath);
            reader.SendWebRequest();
            while (!reader.isDone) { }
            realPath = filepath + "/StarterWaterEarth.dck"; //save the file with this name
            File.WriteAllBytes(realPath, reader.downloadHandler.data);
            Debug.Log("StarterWaterEarth.dck saved in decks folder");
                
            
            #elif UNITY_IOS
            
            realPath = Application.persistentDataPath + "/decks/";
            oriPath = Path.Combine(Application.streamingAssetsPath + "/decks/StarterFireAir.dck"); //returns a DirectoryInfo object
            deck = File.ReadAllBytes(oriPath);
            File.WriteAllBytes(realPath + "StarterFireAir.dck", deck); //save the file with this name
            //File.WriteAllBytes(realPath + "puestaapunto.xlsx", xlsxFile); //alternative of above line, working
            Debug.Log("StarterFireAir.dck saved in decks folder");

            realPath = Application.persistentDataPath + "/decks/";
            oriPath = Path.Combine(Application.streamingAssetsPath + "/decks/StarterWaterEarth.dck"); //returns a DirectoryInfo object
            deck = File.ReadAllBytes(oriPath);
            File.WriteAllBytes(realPath + "StarterWaterEarth.dck", deck); //save the file with this name
            //File.WriteAllBytes(realPath + "puestaapunto.xlsx", xlsxFile); //alternative of above line, working
            Debug.Log("StarterWaterEarth.dck saved in decks folder");
            
            #endif
        }

        decksPath = filepath;
        #endif
    }*/
    
}
