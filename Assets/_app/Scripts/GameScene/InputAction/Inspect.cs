using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Inspect : MonoBehaviour
{
    [SerializeField]
    private InputAction Tap;
    public GameObject inspected;


    private void OnEnable(){
        if(SceneManager.GetActiveScene().name != "GameScene") return;
        Tap.Enable();
        Tap.performed += OnTap;
    }

    private void OnDisable(){
        Tap.performed -= OnTap;
        Tap.Disable();
    }

    private void OnTap(InputAction.CallbackContext context){
        if(!transform.gameObject.activeSelf) return;
        Ray ray = Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        //3d colliders
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if(hit.collider != null && hit.collider.gameObject.GetComponent<IInspect>() != null && hit.collider.gameObject.GetComponent<IInspect>().GetcanInspect()){
                StartCoroutine(InspectObj(hit.collider.gameObject));
            }
        }
        //2d colliders
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
        if (hit2d.collider != null && hit2d.collider.gameObject.GetComponent<IInspect>() != null && hit2d.collider.gameObject.GetComponent<IInspect>().GetcanInspect()){
            StartCoroutine(InspectObj(hit2d.collider.gameObject));
        }
    }

     private IEnumerator InspectObj(GameObject clickedObject){
        if (inspected != null){
            inspected.TryGetComponent<IInspect>(out var inspComponent);
            inspComponent?.onStopInspect();
        }
        clickedObject.TryGetComponent<IInspect>(out var iInspectComponent);
        iInspectComponent?.onStartInspect();
        inspected = clickedObject;
        yield return null;
     }

}
