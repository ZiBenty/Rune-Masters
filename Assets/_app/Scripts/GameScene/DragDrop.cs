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
        //Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        //RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        Collider2D collider = Physics2D.OverlapPoint(Touchscreen.current.primaryTouch.position.ReadValue());
        if (collider != null && (collider.gameObject.layer == LayerMask.NameToLayer("Draggable") || collider.gameObject.GetComponent<IDrag>() != null)){
            StartCoroutine(DragUpdate(collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject){
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag(); //? states: "is that null? If not, run it"
        while (touchPress.ReadValue<float>() != 0) //button is clicked
        {
            Vector3 target = Touchscreen.current.primaryTouch.position.ReadValue();
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, target, ref velocity, dragSpeed);
            yield return null;
        }
        iDragComponent?.onEndDrag();
    }
}
