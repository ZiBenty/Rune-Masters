using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;



public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction touchPress;
    [SerializeField]
    private float dragSpeed = .1f;

    private Camera MainCamera;
    private Vector3 velocity = Vector3.zero;

    private void Awake(){
        MainCamera = Camera.main;
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
        Ray ray = MainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
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
        while (touchPress.ReadValue<float>() != 0) //button is clicked
        {
            var hit = transform.GetComponent<TouchManager>().getHitCollider();
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
