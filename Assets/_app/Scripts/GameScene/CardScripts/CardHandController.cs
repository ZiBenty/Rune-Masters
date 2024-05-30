
using UnityEngine;
using UnityEngine.InputSystem;

public class CardHandController : MonoBehaviour, IDrag, IInspect
{
    [Header("Drag and Drop")]
    [SerializeField]
    private bool canDrag, isDragging;
    private Vector3 _defaultLocalScale, _defaultLocalPosition;

    [Header("Inspect")]
    public bool isInspected = false;
    private float _lastMovement = 0;

    void Awake(){
        SetcanDrag(true);
        SetisDragging(false);
    }

    public void SetcanDrag(bool b){
        canDrag = b;
    }

    public void SetisDragging(bool b){
        isDragging = b;
    }
    
    public bool GetcanDrag(){
        return canDrag;
    }

    public bool GetisDragging(){
        return isDragging;
    }

    public void onStartDrag()
    {
        if(!canDrag) return;

        isDragging = true;

        //saves position and scale to return to if needed
        _defaultLocalScale = transform.localScale;
        _defaultLocalPosition = transform.localPosition;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

        //change collider size
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(col.size.x/10, col.size.y/10);

        Debug.Log("Grabbing");
    }

    public void onDragging()
    {
        if(!canDrag) return;
        //Debug.Log("Dragging...");
    }

    public void onEndDrag()
    {
        if(!canDrag) return;

        isDragging = false;
        Debug.Log("Releasing");

        bool isCardSlot = false;
        CardSlot cardSlot = null;

        //controllo luogo dove finisce
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        foreach(RaycastHit2D hit in hits){
            if(hit.transform.name.Contains("CardSlot")){
                if(hit.transform.childCount == 0){
                    isCardSlot = true;
                    cardSlot = hit.transform.gameObject.GetComponent<CardSlot>();
                }
            }
        }
        //azione differente a seconda di dove finisce
        if(isCardSlot){
            if (cardSlot != null){
                cardSlot?.PlaceCard(GetComponent<DisplayCard>().Card);
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
        Highlight(new Vector3(0, 100, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(GetComponent<DisplayCard>().Card);
    }

    public void onStopInspect()
    {
        isInspected = false;
        Highlight(new Vector3(0, 0, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().HideInfo();
    }

}
