using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropable : MonoBehaviour
{
    [SerializeField]
    private InputAction press, screenPos;
    Camera Camera;

    private Vector2 curScreenPos;
    private Vector2 worldPos
    {
        get
        {
            return Camera.ScreenToWorldPoint(curScreenPos);
        }
    }

    private bool isDragging;
    private bool isClickedOn{
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if(hit.collider != null)
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    private void Awake(){
        Camera = Camera.main;
        press.Enable();
        screenPos.Enable();
        screenPos.performed += context => { curScreenPos = context.ReadValue<Vector2>(); };
        press.performed += _ => { if(isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { isDragging = false; };
    }

    //coroutine drag
    private IEnumerator Drag(){
        isDragging = true;
        Vector2 offset = new Vector2(transform.position.x, transform.position.y) - worldPos;
        //grab
        while(isDragging)
        {
            //dragging
            transform.position = worldPos + offset;
            yield return null;
        }
        //dropping
    }
}
