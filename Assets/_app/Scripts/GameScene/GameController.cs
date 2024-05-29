using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Player player;
    public Player enemy;

    void Awake(){
        Instance = this;
    }
}
