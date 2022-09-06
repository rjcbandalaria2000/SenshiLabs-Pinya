using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public bool swipedRight;
    public bool swipedLeft;

    public int swipeCounter;
    public int swipeRequired;

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
        if(mousePosition.normalized.x < 0)
        {
            // if the mouse is on the left 
            swipedLeft = true;
        }
        if (mousePosition.normalized.x > 0) 
        { 
            
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
