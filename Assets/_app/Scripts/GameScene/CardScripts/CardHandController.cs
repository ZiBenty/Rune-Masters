
using UnityEngine;
using UnityEngine.InputSystem;

public class CardHandController : MonoBehaviour, IDrag, IInspect
{
    [Header("Drag and Drop")]
    public bool canDrag = true;
    public bool isDragging = false;
    private Vector3 defaultLocalScale;
    private Vector3 defaultLocalPosition;

    [Header("Inspect")]
    public bool isInspected = false;
    private float lastMovement = 0;

    void Awake(){
    }

    public void onStartDrag()
    {
        if(!canDrag) return;

        isDragging = true;

        //saves position and scale to return to if needed
        defaultLocalScale = transform.localScale;
        defaultLocalPosition = transform.localPosition;
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
            transform.localPosition = defaultLocalPosition;
            transform.localScale = defaultLocalScale;
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
            moveBy = lastMovement;
        lastMovement = moveBy;

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
