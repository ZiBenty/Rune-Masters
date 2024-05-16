
using UnityEngine;

public class CardController : MonoBehaviour, IDrag, IInspect
{
    public RectTransform rectTransform;
    private Vector3 defaultScale;

    void Awake(){
        defaultScale = rectTransform.localScale;
    }

    public void onStartDrag()
    {
        transform.localScale = new Vector3(.7f, .7f, .7f);
        Debug.Log("Dragging...");
    }

    public void onEndDrag()
    {
        transform.localScale = defaultScale;
        Debug.Log("Releasing");
    }

    public void onInspect()
    {
        GameObject box = GameObject.Find("CardInspectionBox");
        box?.GetComponent<CardInspectionBox>().ShowInfo(GetComponent<DisplayCard>().Card);
    }

}
