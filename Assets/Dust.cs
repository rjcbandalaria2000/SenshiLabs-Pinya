using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [Header("States")]
    public bool swipedRight;
    public bool swipedLeft;

    [Header("Values")]
    public int swipeCounter;
    public int swipeRequired;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float swipeLeftAccept = -0.5f;
    [Range(0f, 1f)] 
    public float swipeRightAccept = 0.5f;

    private Camera mainCamera; 

    // Start is called before the first frame update
    void Start()
    {
        swipedRight = false;
        swipedLeft = false; 
        mainCamera = Camera.main;
    }

    private void OnMouseDrag()
    {
        //can be improved 
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(mousePosition.normalized.x < swipeLeftAccept)
        {
            // if the mouse moved to the left
            swipedLeft = true;
        }
        if (mousePosition.normalized.x > swipeRightAccept) 
        { 
            // if the mouse moved to the right 
            swipedRight = true;
        
        }
        if(swipedRight && swipedLeft)
        {
            swipeCounter++;
            swipedLeft = false;
            swipedRight = false;
        }
        if(swipeCounter >= swipeRequired)
        {
            Destroy(this.gameObject);
        }
        Debug.Log("X coordinate: " + mousePosition.normalized.x);
    }


}
