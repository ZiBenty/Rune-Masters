using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{

    private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.transform.name.Contains("CardVisualHand"))
            outline.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other){
        outline.enabled = false;
    }
}
