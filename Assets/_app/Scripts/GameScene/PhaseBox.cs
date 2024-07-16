using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

//Logic handling the behaviour of the boxes that indicate the current turn phase
public class PhaseBox : MonoBehaviour
{
    private TurnSystem _ts;
    private string _phaseName;
    private bool _isPhase;

    void Start(){
        _ts = TurnSystem.Instance;
        _phaseName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        //gets right variable with Reflection
        _isPhase = (bool)_ts.GetType().GetField("is" + _phaseName).GetValue(_ts);
        
        if(_isPhase)
            GetComponent<Image>().color = Color.green;
        else
            GetComponent<Image>().color = Color.white;
    }
}
