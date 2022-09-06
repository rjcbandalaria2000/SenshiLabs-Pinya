using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    //Code Reference: https://www.youtube.com/watch?v=Tv82HIvKcZQ
    Vector3 dragOffset;
    

    Vector3 GetMousePosition()
    {
        //takes the position of the mouse in screen space to world space coordinates 
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;

    } 
    private void OnMouseDown()
    {
        // add offset so the object will not snap in the pivot point of the object
        dragOffset = transform.position - GetMousePosition();
        dragOffset.z = 0;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + dragOffset;
    }
    private void OnMouseOver()
    {
        Debug.Log("Over an object");
    }
}
