using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class CardState : MonoBehaviour
{
    public Player Owner;
    public Player Controller;
    private Location m_Location;
    public Location Location{
        get { return m_Location; }
        set{
            if (m_Location == value) return;
            m_Location = value;
            if (OnLocationChange != null)
                OnLocationChange(m_Location);
        }
    }

    public delegate void OnLocationChangeDelegate(Location loc);
    public event OnLocationChangeDelegate OnLocationChange;

    public void SetPlayer(Player player){
        Owner = player;
        Controller = player;
    }

    public void ResetPlayer(){
        Controller = Owner;
    }

}
