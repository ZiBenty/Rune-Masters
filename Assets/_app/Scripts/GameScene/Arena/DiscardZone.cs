using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardZone : MonoBehaviour
{
    [SerializeField]
    public GameObject visualPrefab;
    private Image _visual;
    private int _lastPileSize = 0;
    public Player Owner;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.name == "PlayerDiscardZone")
            Owner = GameManager.Instance.player;
        else
            Owner = GameManager.Instance.enemy;
    }

    void Update()
    {
        if(transform.childCount != 0){
            _visual.enabled = false;
        }else if (_lastPileSize != 0){
            _visual.enabled = true;
        }
        if(_lastPileSize != transform.childCount){
            ShowLastCard();
        }
        

    }

    public void AddCard(GameObject card){
        GameObject copy = Instantiate(card);
        copy.GetComponent<CardInfo>().LoadInfo(card.GetComponent<CardInfo>().BaseInfo);
        if (copy.transform.childCount != 0)
            Destroy(copy.transform.GetChild(0).gameObject); // removes visual from copy object
        
        //adds the card to the discrad pile
        copy.transform.SetParent(transform, false);
        copy.transform.SetAsFirstSibling();
        
        GameObject cv = Instantiate(visualPrefab, copy.transform);
        cv.transform.localScale = new Vector3(0.16f, 0.16f);
        cv.GetComponent<CardDisplay>().LoadCard();

        copy.GetComponent<CardState>().Location = Constants.Location.Discard; 
    }

    public void ShowLastCard(){
        if(transform.childCount != 0){
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true); 
            for(int i = 1; i < transform.childCount; i++)
                transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false); 
        }
        _lastPileSize = transform.childCount;
    }
    
}
