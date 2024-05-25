using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Column0, Column1, Column2, Column3, Column4;

    public List<List<GameObject>> Columns;

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
}
