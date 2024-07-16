using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class CardState : MonoBehaviour
{
    public Player Owner {get; set;}
    public Player Controller;
    public Location Location;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayer(Player player){
        Owner = player;
        Controller = player;
    }

    public void ResetPlayer(){
        Controller = Owner;
    }

}
