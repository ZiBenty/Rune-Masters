
using UnityEngine;

public class CardHandController : MonoBehaviour, IDrag, IInspect
{
    private TouchManager tm;
    private Vector3 defaultScale;
    private bool isOnHoverPlane;
    //private Vector3 highlightVelocity = Vector3.zero;

    void Awake(){
        defaultScale = transform.localScale;
        isOnHoverPlane = false;
        tm = GameObject.Find("TouchManager").GetComponent<TouchManager>();
    }

    public void onStartDrag()
    {
        Debug.Log("Grabbing");
    }

    public void onDragging()
    {
        RaycastHit hit = tm.getHitCollider();
        if (hit.transform.name.Contains("Hand"))
        {
            transform.localScale = defaultScale * 1.6f;
            isOnHoverPlane = false;
        }
        else if (hit.transform.name == "HoverPlane")
        {
            if (!isOnHoverPlane)
                transform.localScale = defaultScale * 2.2f;
            isOnHoverPlane = true;
        }
        //Debug.Log("Dragging...");
    }

    public void onEndDrag()
    {
        //transform.localScale = defaultScale;
        //transform.parent.transform.name = 
        //fare opzione per se finisce in mano o sul terreno
        Debug.Log("Releasing");
    }

    public void Highlight(Vector3 target)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, 150);
    }

    public void onInspect()
    {
        Highlight(new Vector3(0, 150, 0));
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(GetComponent<DisplayCard>().Card);
    }

}
