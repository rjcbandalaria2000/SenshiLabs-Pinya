using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [Header("States")]
    public bool     swipedRight;
    public bool     swipedLeft;

    [Header("Values")]
    public int      SwipeRequired;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float    SwipeLeftAccept = -0.5f;
    [Range(0f, 1f)] 
    public float    SwipeRightAccept = 0.5f;

    private Camera mainCamera; 
    private int swipeCounter;
    private Vector2 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        swipedRight = false;
        swipedLeft = false; 
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        initialPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        //can be improved to be transfered in Sweeping Controls
        //Get mouse position
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)initialPosition;
        if(mousePosition.normalized.x < SwipeLeftAccept)
        {
            // if the mouse moved to the left
            swipedLeft = true;
        }
        if (mousePosition.normalized.x > SwipeRightAccept) 
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
        if(swipeCounter >= SwipeRequired)
        {
            SingletonManager.Get<CleanTheHouseManager>().AddDustSwept(1);
            Destroy(this.gameObject);
        }
        //Debug.Log("X coordinate: " + mousePosition.normalized.x);
    }


}
