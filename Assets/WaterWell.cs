using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWell : MonoBehaviour
{
    [Header("Values")]
    public int      RequiredSwipes;

    [Header("States")]
    public bool     SwipedUp;
    public bool     CanSwipeUp; // blocks the player from swiping up early on the level
    public bool     SwipedDown;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, 1f)]
    public float    SwipeUpAccept = 0.5f;
    [Range(0f, -1f)]
    public float    SwipeDownAccept = -0.5f;

    private int     playerSwipeUpCount;
    private int     playerSwipeDownCount;
    private Camera  mainCamera;
    private Vector2 initialMousePosition;
 
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        mainCamera = Camera.main;
        RequiredSwipes = SingletonManager.Get<GetWaterManager>().RequiredNumSwipes;
    }

    public void OnMouseDown()
    {
        initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)initialMousePosition  ;
        if (!SwipedUp)
        {
            if (mousePosition.normalized.y < SwipeDownAccept)
            {

                if (!SwipedDown)
                {
                    SwipedDown = true;
                    playerSwipeDownCount++;
                }
                if (!CanSwipeUp)
                {
                    CanSwipeUp = true; // enable swiping up when the player swiped down first
                }

            }
        }
        if (!SwipedDown)
        {
            if (CanSwipeUp)
            {
                if (mousePosition.normalized.y > SwipeUpAccept)
                {
                    if (!SwipedUp)
                    {
                        SwipedUp = true;
                        playerSwipeUpCount++;
                    }

                }
            }
        }
        Debug.Log("Y coordinate: " + mousePosition.normalized.y);
    }

    private void OnMouseUp()
    {
        if (SwipedDown)
        {
            SwipedDown = false;
        }
        if (SwipedUp) {

            SwipedUp = false;
        
        }
    }

}
