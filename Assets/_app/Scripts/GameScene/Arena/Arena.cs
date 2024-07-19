using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class Arena : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Column0, Column1, Column2, Column3, Column4;

    public List<List<GameObject>> Columns;

    public List<ArenaLine> Lines;

    public void Awake(){
        Columns = new List<List<GameObject>>
        {
            Column0,
            Column1,
            Column2,
            Column3,
            Column4
        };
    }
    
    //TODO: complete correctly
    public List<List<Rune>> getArenaMatrix(){
        List<GameObject> l = new List<GameObject>();
        foreach(ArenaLine line in Lines){
            for(int i=0; i<line.transform.childCount; i++){
                TempRunes tr = line.transform.GetChild(i).GetChild(1).GetComponent<TempRunes>();
                if(tr.Owner != null && tr.Owner == transform.GetComponent<CardState>().Controller){
                    l.Add(line.transform.GetChild(i).GetChild(1).gameObject);
                }
            }
        }
        return l;
    }
}
