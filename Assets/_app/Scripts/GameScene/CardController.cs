
using UnityEngine;

public class CardController : MonoBehaviour, IDrag, IInspect
{
    private Vector3 defaultScale;

    //private Vector3 highlightVelocity = Vector3.zero;

    void Awake(){
        defaultScale = transform.localScale;
    }

    public void onStartDrag()
    {
        Debug.Log("Grabbing");
    }

    public void onDragging()
    {
        
        Debug.Log("Dragging...");
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
