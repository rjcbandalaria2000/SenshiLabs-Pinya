using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    //Code Reference: https://www.youtube.com/watch?v=Tv82HIvKcZQ
    private Vector3 dragOffset;
    private Camera  mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    Vector3 GetMousePosition()
    {
        //takes the position of the mouse in screen space to world space coordinates 
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;

    } 
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        // add offset so the object will not snap in the pivot point of the object
        dragOffset = transform.position - GetMousePosition();
        dragOffset.z = 0;
        Events.OnMouseDown.Invoke();
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        transform.position = GetMousePosition() + dragOffset;
    }
    private void OnMouseOver()
    {
        //Debug.Log("Over an object");
    }
}
