using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class HealthComponent : MonoBehaviour
{
    private int m_Hp;
    public int Hp{
        get { return m_Hp; }
        set{
            if (m_Hp == value) return;
            if (value > 10)
                m_Hp = 10;
            else
                m_Hp = value;
            if (OnHpChange != null)
                OnHpChange(m_Hp);
        }
    }
    public delegate void OnHpChangeDelegate(int hp);
    public event OnHpChangeDelegate OnHpChange;

    // Start is called before the first frame update
    void Start()
    {
        OnHpChange += HealthChange;
    }

    private void HealthChange(int hp){
        transform.GetComponent<CardInfo>().TempInfo.Hp = hp;
        transform.GetChild(0).GetComponent<CardDisplay>().LoadCard();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Hp <= 0){
            DestroyCard();
        }
    }

    public void DestroyCard(){
        GameManager.Instance.MoveLocation(transform.gameObject, Location.Discard);
    }
}
