using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inspect : MonoBehaviour
{
    [SerializeField]
    private InputAction tap;

    private Camera mainCamera;
    [SerializeField]
    private Plane hoverPlane;

    private void Awake(){
        mainCamera = Camera.main;
    }

    private void OnEnable(){
        tap.Enable();
        tap.performed += OnTap;
    }

    private void OnDisable(){
        tap.performed -= OnTap;
        tap.Disable();
    }

    private void OnTap(InputAction.CallbackContext context){
        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        //3d colliders
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if(hit.collider != null && hit.collider.gameObject.GetComponent<IInspect>() != null){
                StartCoroutine(InspectObj(hit.collider.gameObject));
            }
        }
        //2d colliders
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        if (hit2d.collider != null && (hit2d.collider.gameObject.layer == LayerMask.NameToLayer("Draggable") || hit2d.collider.gameObject.GetComponent<IDrag>() != null)){
            StartCoroutine(InspectObj(hit2d.collider.gameObject));
        }
    }

     private IEnumerator InspectObj(GameObject clickedObject){
        clickedObject.TryGetComponent<IInspect>(out var iInspectComponent);
        iInspectComponent?.onInspect();
        yield return null;
     }

}
