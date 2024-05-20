using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction touchPress;
    [SerializeField]
    private float dragSpeed = .1f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private void Awake(){
        mainCamera = Camera.main;
    }

    private void OnEnable(){
        touchPress.Enable();
        touchPress.performed += OnTouchPress;
    }

    private void OnDisable(){
        touchPress.performed -= OnTouchPress;
        touchPress.Disable();
    }

    private void OnTouchPress(InputAction.CallbackContext context){
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        //Collider2D collider = Physics2D.OverlapPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        if (hit2d.collider != null && (hit2d.collider.gameObject.layer == LayerMask.NameToLayer("Draggable") || hit2d.collider.gameObject.GetComponent<IDrag>() != null)){
            StartCoroutine(DragUpdate(hit2d.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject){
        var defaultPos = clickedObject.transform.position;
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag(); //? states: "is that null? If not, run it"
        Vector3 lastTouch;
        while (touchPress.ReadValue<float>() != 0) //button is clicked
        {
            lastTouch = Touchscreen.current.primaryTouch.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(lastTouch);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Vector3 target = hit.point;
            clickedObject.transform.eulerAngles = hit.transform.eulerAngles;
            iDragComponent?.onDragging();
            if (clickedObject.transform.position != defaultPos)
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref velocity, dragSpeed);
            else
                clickedObject.transform.position = hit.point;
            yield return null;
        }
        iDragComponent?.onEndDrag();
    }
}
