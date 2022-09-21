using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWell : MonoBehaviour
{
    [Header("Values")]
    public int      RequiredSwipes;

    [Header("States")]
    public bool     SwipedUp;
    public bool     SwipedDown;
    public bool     CanSwipeUp; // blocks the player from swiping up early on the level
    public bool     CanSwipeDown;

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
        CanSwipeDown = true;
        CanSwipeUp = false;
        SwipedDown = false;
        SwipedUp = false;

        SingletonManager.Get<GetWaterManager>().slider.maxValue = RequiredSwipes;
        SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
    }

    public void OnMouseDown()
    {
        //Get initial position of the mouse 
        initialMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        // use the initial position of the mouse then subract it to its current position to get the direction 
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)initialMousePosition;
        if (CanSwipeDown)
        {
            if (!SwipedUp) //if already swiped up, do not let the player swipe down
            {
                if (mousePosition.normalized.y < SwipeDownAccept)
                {

                    if (!SwipedDown)
                    {
                        SwipedDown = true;
                        playerSwipeDownCount++;
                        SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
                        Events.OnObjectiveUpdate.Invoke();
                    }
                    if (!CanSwipeUp)
                    {
                        CanSwipeUp = true; // enable swiping up when the player swiped down first
                    }

                }
            }
        } 
       
        if (!SwipedDown) // if already swiped down, do not let the player swipe up 
        {
            if (CanSwipeUp)
            {
                if (mousePosition.normalized.y > SwipeUpAccept)
                {
                    if (!SwipedUp)
                    {
                        // lock the controls of the player since the player lifted the bucket
                        SwipedUp = true;
                        CanSwipeDown = false;
                        CanSwipeUp = false; 
                        playerSwipeUpCount++;
                        Events.OnObjectiveUpdate.Invoke();
                    }

                }
            }
        }
        if(playerSwipeUpCount >= 1)
        {
            SingletonManager.Get<GetWaterManager>().SetNumOfSwipes(playerSwipeDownCount);
            SingletonManager.Get<GetWaterManager>().CheckIfComplete();
        }
     //   Debug.Log("Y coordinate: " + mousePosition.normalized.y);
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

    public int GetSwipeDown()
    {
        return playerSwipeDownCount;
    }
    

}
