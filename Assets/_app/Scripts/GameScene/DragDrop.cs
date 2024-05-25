using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;



public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction touchPress;
    [SerializeField]
    private float dragSpeed = .1f, dragPhysicsSpeed = 10;

    private Camera MainCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
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
        if (hit2d.collider != null && (hit2d.collider.gameObject.layer == LayerMask.NameToLayer("Draggable") || hit2d.collider.gameObject.GetComponent<IDrag>() != null)){
            StartCoroutine(DragUpdate(hit2d.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject){
        float initialDistance = Vector3.Distance(clickedObject.transform.position, MainCamera.transform.position);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        iDragComponent?.onStartDrag(); //? states: "is that null? If not, run it"
        while (touchPress.ReadValue<float>() != 0) //button is clicked
        {
            Ray ray = MainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
                if (rb != null){
                    Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                    rb.velocity = direction * dragPhysicsSpeed;
                    iDragComponent?.onDragging(); 
                    yield return waitForFixedUpdate;
                }else{
                    clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, dragSpeed);
                    iDragComponent?.onDragging(); 
                    yield return null;
                }
        }
        if (rb != null){
            rb.velocity = Vector3.zero;
        }
        iDragComponent?.onEndDrag();
    }
}
