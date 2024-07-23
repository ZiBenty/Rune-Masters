using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction TouchPress;
    [SerializeField]
    private float DragSpeed, DragPhysicsSpeed;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector3 _velocity = Vector3.zero;

    private void OnEnable(){
        if(SceneManager.GetActiveScene().name != "GameScene") return;
        TouchPress.Enable();
        TouchPress.performed += OnTouchPress;
    }

    private void OnDisable(){
        TouchPress.performed -= OnTouchPress;
        TouchPress.Disable();
    }

//hit2d.collider.gameObject.layer == LayerMask.NameToLayer("Draggable")

    private void OnTouchPress(InputAction.CallbackContext context){
        if(!transform.gameObject.activeSelf) return;
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        if (hit2d.collider != null && hit2d.collider.gameObject.GetComponent<IDrag>() != null && hit2d.collider.gameObject.GetComponent<IDrag>().GetcanDrag()){
            StartCoroutine(DragUpdate(hit2d.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject){
        float initialDistance = Vector3.Distance(clickedObject.transform.position, Camera.main.transform.position);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        clickedObject.TryGetComponent<Rigidbody2D>(out var rb);
        iDragComponent?.onStartDrag(); //? states: "is that null? If not, run it"
        while (TouchPress.ReadValue<float>() != 0) //button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
                if (rb != null){
                    Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                    rb.velocity = direction * DragPhysicsSpeed;
                    iDragComponent?.onDragging(); 
                    yield return _waitForFixedUpdate;
                }else{
                    clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref _velocity, DragSpeed);
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
