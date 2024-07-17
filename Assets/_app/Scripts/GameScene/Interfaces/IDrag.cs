using UnityEngine;

public interface IDrag{
    public void SetcanDrag(bool b);
    public void SetisDragging(bool b);
    public bool GetcanDrag();
    public bool GetisDragging();

    void onStartDrag();
    void onDragging();
    void onEndDrag();
}