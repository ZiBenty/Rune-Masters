
using UnityEngine;

public class CardController : MonoBehaviour, IDrag
{
    private Vector3 defaultScale = new Vector3(.5f, .5f, .5f);
    public void onStartDrag()
    {
        transform.localScale = new Vector3(.7f, .7f, .7f);
    }

    public void onEndDrag()
    {
        transform.localScale = defaultScale;
    }



}
