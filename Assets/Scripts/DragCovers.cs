using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCovers : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 dragOffset;
  

    void Start()
    {
        mainCamera = Camera.main;
       


    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                // add offset so the object will not snap in the pivot point of the object
                dragOffset = transform.position - GetMousePosition();
                dragOffset.z = 0;
                Events.OnMouseDown.Invoke();
            }
        }    
    }

    Vector3 GetMousePosition()
    {
        //takes the position of the mouse in screen space to world space coordinates 
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;

    }

    //private void OnMouseDown()
    //{
    //    RaycastHit hit;
    //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit, 1000f, ~IgnoreLayer))
    //    {

    //        if (hit.transform.gameObject.tag == "HidingSpot")
    //        {
    //            // add offset so the object will not snap in the pivot point of the object
    //            dragOffset = transform.position - GetMousePosition();
    //            dragOffset.z = 0;
    //            Events.OnMouseDown.Invoke();
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }

            
    //}

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        transform.position = GetMousePosition() + dragOffset;
    }

   
}
