using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class TargetHandler : MonoBehaviour
{
    public static TargetHandler Instance { get; private set;}
    public List<GameObject> Targets;
    public bool TargetMode = false;
    private int NumOfTargets;
    public bool ConfirmMode = false;

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
        OnEnable();
    }

    public void OnEnable(){
        Targets = new List<GameObject>();
    }

    public void StartTargetMode(int num, bool confirm){
        TargetMode = true;
        NumOfTargets = num;
        Targets = new List<GameObject>();
        if(confirm){
            ConfirmMode = confirm;
            UIManager.Instance.SetActiveConfirmButtons(true);
        }
    }

    public void EndTargetMode(){
        TargetMode = false;
        if(ConfirmMode){
            ConfirmMode = false;
            UIManager.Instance.SetActiveConfirmButtons(false);
        }
    }

    public void AddTarget(GameObject target){
        if (OnAddTarget(target)){
            if (ConfirmMode && Targets.Count == NumOfTargets){
                Targets.RemoveAt(0); //removes oldest target
                Targets.Add(target);
                if (target.TryGetComponent<Outline>(out var outline))
                    outline.enabled = true;
            }else
                Targets.Add(target);
        }
    }

    public void OnConfirmYes(){
        EndTargetMode();
    }

    public void OnConfirmNo(){
        Targets.Clear();
        EndTargetMode();
    }

    void Update(){
        if (!ConfirmMode){
            if (TargetMode && Targets.Count == NumOfTargets)
                EndTargetMode();
        }
    }

}

