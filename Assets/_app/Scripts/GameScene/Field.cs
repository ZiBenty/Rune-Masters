using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Column0, Column1, Column2, Column3, Column4;

    public List<List<GameObject>> Columns;

    public void Awake(){
        Columns = new List<List<GameObject>>();
        Columns.Add(Column0);
        Columns.Add(Column1);
        Columns.Add(Column2);
        Columns.Add(Column3);
        Columns.Add(Column4);
    }
}
