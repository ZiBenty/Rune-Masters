using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class TargetHandler : MonoBehaviour
{
    public static TargetHandler Instance { get; private set;}
    public List<GameObject> Targets;
    public bool TargetMode = false;
    private int NumOfTargets;

    public delegate bool OnAddTargetDelegate(GameObject target);
    public event OnAddTargetDelegate OnAddTarget;
    

    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        Targets = new List<GameObject>();
    }

    public void StartTargetMode(int num){
        TargetMode = true;
        NumOfTargets = num;
        Targets = new List<GameObject>();
    }

    public void EndTargetMode(){
        TargetMode = false;
    }

    public void AddTarget(GameObject target){
        if (OnAddTarget(target)){
            Targets.Add(target);
        }
    }

    void Update(){
        if (TargetMode && Targets.Count == NumOfTargets){
            EndTargetMode();
        }
    }

}
