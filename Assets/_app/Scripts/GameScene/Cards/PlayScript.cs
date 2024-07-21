using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Constants;

//handles how the card responds to direct player input
public class PlayScript : MonoBehaviour, IDrag, IInspect
{
    [Header("Drag and Drop")]
    [SerializeField]
    private bool _canDrag, _isDragging;
    private Vector3 _defaultLocalScale, _defaultLocalPosition;

    [Header("Inspect")]
    private bool _canInspect;
    public bool isInspected = false;
    private float _lastMovement = 0;

    private TurnSystem _ts;
    private ArenaCardSlot CardSlot = null;
    public bool isMoving = false;

    void Awake(){
        SetcanDrag(true);
        SetisDragging(false);
        transform.GetComponent<CardState>().OnLocationChange += LocationChangeHandler;
    }

    void Start(){
        _ts = TurnSystem.Instance;
    }

    public void SetcanDrag(bool b){
        _canDrag = b;
    }
    public void SetcanInspect(bool b){
        _canInspect = b;
    }

    public void SetisDragging(bool b){
        _isDragging = b;
    }
    
    public bool GetcanDrag(){
        return _canDrag;
    }

    public bool GetcanInspect(){
        return _canInspect;
    }

    public bool GetisDragging(){
        return _isDragging;
    }

    public void onStartDrag()
    {
        if(!_canDrag) return;

        _isDragging = true;

        //saves position and scale to return to if needed
        _defaultLocalScale = transform.localScale;
        _defaultLocalPosition = transform.localPosition;
        //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        //change collider size
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(col.size.x/15, col.size.y/15);

        Debug.Log("Grabbing");
        
        if (transform.GetComponent<CardState>().Location == Location.Field){ //if it was dragged from field
            if(transform.GetComponent<MoveComponent>().CanItBeMoved()){
                transform.GetComponent<MoveComponent>().ColorAvailableSlots(true);
                isMoving = true;
                UIManager.Instance.ChangeHintBox(true, "You can move the creature in the outlined slots");
            }else{
                StartCoroutine(UIManager.Instance.HintForSeconds("Creatures can only move once per turn", 3f));
            }
        }
    }

    public void onDragging()
    {
        if(!_canDrag) return;
        //Debug.Log("Dragging...");
    }

    public void onEndDrag()
    {
        if(!_canDrag) return;

        _isDragging = false;
        Debug.Log("Releasing");

        ArenaCardSlot cardSlot = null;

        //controllo luogo dove finisce
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        foreach(RaycastHit2D hit in hits){
            if(hit.transform.name.Contains("CardSlot")){
                if (transform.GetComponent<CardState>().Location == Location.Hand){ //if it was dragged from hand
                    //controllo evocazione per tipi
                    ArenaLine summonLine;
                    switch(transform.GetComponent<CardInfo>().TempInfo.CardType){
                        case CardType.Creature:
                            //only on summonLine
                            summonLine = transform.GetComponent<CardState>().Controller.SummonLine.GetComponent<ArenaLine>();

                            if(summonLine.IsSlotInLine(hit.transform.parent.gameObject)){
                                if(hit.transform.childCount == 0) //if slot is free
                                    cardSlot = hit.transform.gameObject.GetComponent<ArenaCardSlot>();
                            }else{
                                StartCoroutine(UIManager.Instance.HintForSeconds("Creatures can only be casted in Controller's Summon Line", 3f));
                            }
                            break;
                        case CardType.Structure:
                            //only on player lines and middle line
                            summonLine = transform.GetComponent<CardState>().Controller.SummonLine.GetComponent<ArenaLine>();

                            ArenaLine frontLine;
                            if (transform.GetComponent<CardState>().Controller.transform.name == "Player")
                                frontLine = GameObject.Find("Front Player Line").GetComponent<ArenaLine>();
                            else
                                frontLine = GameObject.Find("Front Enemy Line").GetComponent<ArenaLine>();

                            ArenaLine middleLine = GameObject.Find("Middle Line").GetComponent<ArenaLine>();

                            if(summonLine.IsSlotInLine(hit.transform.parent.gameObject) || frontLine.IsSlotInLine(hit.transform.parent.gameObject) || middleLine.IsSlotInLine(hit.transform.parent.gameObject)){
                                if(hit.transform.childCount == 0) //if slot is free
                                    cardSlot = hit.transform.gameObject.GetComponent<ArenaCardSlot>();
                            }else{
                                StartCoroutine(UIManager.Instance.HintForSeconds("Structures cannot be casted in the other player's Lines", 3f));
                            }
                            break;
                        case CardType.Enchantment:
                            break;
                    }
                    if(transform.GetComponent<CastComponent>().CanItBeCasted()){
                        if(transform.GetComponent<CastComponent>().CastCard())
                            CardSlot = cardSlot; //saves cardSlot position to be saved later
                        else
                            ResetPosition();
                    }else{
                        ResetPosition();
                    }
                }
                else if (transform.GetComponent<CardState>().Location == Location.Field){ //if it was dragged from field
                    cardSlot = hit.transform.gameObject.GetComponent<ArenaCardSlot>();
                    if (transform.GetComponent<MoveComponent>().AvailableSlots.Contains(cardSlot.transform.parent.GetComponent<Slot>())){
                        CardSlot = cardSlot;
                        transform.GetComponent<MoveComponent>().SetCanBeMoved(false);
                        transform.GetComponent<MoveComponent>().ColorAvailableSlots(false);
                        UIManager.Instance.ChangeHintBox(false);
                        PlaceInSlot();
                    } else{
                        transform.GetComponent<MoveComponent>().ColorAvailableSlots(false);
                        UIManager.Instance.ChangeHintBox(false);
                        ResetPosition();
                    }
                    isMoving = false;
                }
            }
        }

        if (cardSlot == null){ //if nothing happened, reset card position
            ResetPosition();
        }
        
    }

    public void PlaceInSlot(){
        if(transform.GetComponent<CardInfo>().TempInfo.CardType == CardType.Enchantment){
                StartCoroutine(GameManager.Instance.MoveLocation(transform.gameObject, Location.Discard));
        }
        else if(CardSlot != null){
                CardSlot?.PlaceCard(transform.gameObject);
                Destroy(gameObject);
        }
    }

    private void ResetPosition(){
        transform.localPosition = _defaultLocalPosition;
        transform.localScale = _defaultLocalScale;
        //change collider size
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(col.size.x*15, col.size.y*15); //dragging reduces objects boxcolliders to prevent multiple collisions
    }

    public void Highlight(Vector3 target)
    {
        float moveBy;
        if(isInspected)
            moveBy = target.y;
        else
            moveBy = _lastMovement;
        _lastMovement = moveBy;

        if (transform.parent.parent.transform.name == "PlayerHand")
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, moveBy);
        else
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, -target, moveBy);

    }

    public void onStartInspect()
    {
        isInspected = true;
        if(TargetHandler.Instance.TargetMode){
            TargetHandler.Instance.AddTarget(transform.gameObject);
        }
        if (transform.GetComponentInChildren<CardState>().Location == Location.Hand)
            Highlight(new Vector3(0, 100, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(transform.gameObject);
    }

    public void onStopInspect()
    {
        isInspected = false;
        if (transform.GetComponentInChildren<CardState>().Location == Location.Hand)
            Highlight(new Vector3(0, 0, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().HideInfo();
    }

    void OnDestroy(){
        if (transform.GetComponentInChildren<CardState>().Location == Constants.Location.Hand ||
        transform.GetComponentInChildren<CardState>().Location == Constants.Location.Field)
            Destroy(transform.parent.gameObject);
    }

    void LocationChangeHandler(Location loc){
        switch (loc){
            case Location.Deck:
                SetcanDrag(false);
                SetcanInspect(false);
                break;
            case Location.Hand:
                SetcanDrag(true);
                SetcanInspect(true);
                transform.GetComponent<BoxCollider2D>().enabled = true;
                transform.GetComponent<BoxCollider2D>().size = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta*transform.GetChild(0).GetComponent<RectTransform>().localScale;
                break;
            case Location.Field:
                if (transform.GetComponent<CardInfo>().TempInfo.Id !=0 && transform.GetComponent<CardInfo>().TempInfo.CardType == CardType.Creature)
                    SetcanDrag(true);
                else
                    SetcanDrag(false);
                SetcanInspect(true);
                transform.GetComponent<BoxCollider2D>().enabled = true;
                transform.GetComponent<BoxCollider2D>().size = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta*new Vector3(0.16f, 0.16f, 0.16f);
                transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (transform.GetComponent<CardState>().Controller.transform.name == "Enemy")
                    transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Location.Discard:
                SetcanDrag(false);
                SetcanInspect(false);
                transform.GetComponent<BoxCollider2D>().enabled = false;
                if (transform.GetComponent<CardState>().Controller.transform.name == "Enemy")
                    transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Location.Inspected:
                SetcanDrag(false);
                SetcanInspect(false);
                GetComponent<BoxCollider2D>().enabled = false;
                break;
        }
    }
}
