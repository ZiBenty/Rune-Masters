using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void Awake(){
        SetcanDrag(true);
        SetisDragging(false);
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
        col.size = new Vector2(col.size.x/10, col.size.y/10);

        Debug.Log("Grabbing");
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

        bool isCardSlot = false;
        ArenaCardSlot cardSlot = null;

        //controllo luogo dove finisce
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        foreach(RaycastHit2D hit in hits){
            if(hit.transform.name.Contains("CardSlot")){
                if(hit.transform.childCount == 0){
                    isCardSlot = true;
                    cardSlot = hit.transform.gameObject.GetComponent<ArenaCardSlot>();
                }
            }
        }
        //azione differente a seconda di dove finisce
        if(isCardSlot){
            if (cardSlot != null){
                cardSlot?.PlaceCard(transform.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            transform.localPosition = _defaultLocalPosition;
            transform.localScale = _defaultLocalScale;
            //change collider size
            BoxCollider2D col = GetComponent<BoxCollider2D>();
            col.size = new Vector2(col.size.x*10, col.size.y*10);
        }
        
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
        if (transform.GetComponentInChildren<CardState>().Location == Constants.Location.Hand)
            Highlight(new Vector3(0, 100, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(transform.gameObject);
    }

    public void onStopInspect()
    {
        isInspected = false;
        if (transform.GetComponentInChildren<CardState>().Location == Constants.Location.Hand)
            Highlight(new Vector3(0, 0, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().HideInfo();
    }

    void OnDestroy(){
        if (transform.GetComponentInChildren<CardState>().Location == Constants.Location.Hand ||
        transform.GetComponentInChildren<CardState>().Location == Constants.Location.Field)
            Destroy(transform.parent.gameObject);
    }
}
