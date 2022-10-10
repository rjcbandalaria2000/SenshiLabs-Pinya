using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWell : MonoBehaviour
{
    [Header("Values")]
    public int              RequiredSwipes;
    public FillWaterBucket  fillWaterBucket;

    [Header("States")]
    public bool             SwipedUp;
    public bool             SwipedDown;
    public bool             CanSwipeUp; // blocks the player from swiping up early on the level
    public bool             CanSwipeDown;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, 1f)]
    public float            SwipeUpAccept = 0.5f;
    [Range(0f, -1f)]
    public float            SwipeDownAccept = -0.5f;

    [Header("Buckets Filled")]
    public List<float>      waterBuckets = new();
    public int              availableBuckets = 3;

    private int             playerSwipeUpCount;
    private int             playerSwipeDownCount;
    private Camera          mainCamera;
    private Vector2         initialMousePosition;
 
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
        if(availableBuckets <= 0) { return; }
        if (SwipedUp) { return; } //if already swiped up, do not let the player swipe down
        if (CanSwipeDown)
        {
            if (mousePosition.normalized.y < SwipeDownAccept)
            {
                if (!SwipedDown)
                {
                    SwipedDown = true;
                    if (playerSwipeDownCount < RequiredSwipes)
                    {
                        playerSwipeDownCount++;
                        SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
                        Events.OnObjectiveUpdate.Invoke();
                    }
                    if (playerSwipeDownCount >= RequiredSwipes)
                    {
                        if(fillWaterBucket == null) { return; }
                        fillWaterBucket.StartFillingBucket();
                    }
                   
                }
                if (!CanSwipeUp)
                {
                    CanSwipeUp = true; // enable swiping up when the player swiped down first
                }

            }
            
        }

        if (CanSwipeUp)
        {
            if (mousePosition.normalized.y > SwipeUpAccept)
            {
                // Stop the water from filling 
                fillWaterBucket.StopFillingBucket();
                if (availableBuckets > 0)
                {
                    //Store the waterAmount in the waterBuckets 
                    waterBuckets.Add(fillWaterBucket.waterAmount);
                    //Reset the waterAmount 
                    fillWaterBucket.ResetWaterBucket();
                    //subtract 1 fro the available buckets 
                    availableBuckets--;
                    //Reset swipes 
                    playerSwipeDownCount = 0;
                    SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
                }
                if(availableBuckets <= 0)
                {
                    Debug.Log("No more buckets");
                    SingletonManager.Get<GetWaterManager>().CheckIfComplete();
                }
                CanSwipeUp = false;


                //if (!SwipedUp)
                //{
                //    // lock the controls of the player since the player lifted the bucket
                //    SwipedUp = true;
                //    CanSwipeDown = false;
                //    CanSwipeUp = false;
                //    playerSwipeUpCount++;
                //    Events.OnObjectiveUpdate.Invoke();
                //}

            }
        }
        

        //if(playerSwipeUpCount >= 1)
        //{
        //    SingletonManager.Get<GetWaterManager>().SetNumOfSwipes(playerSwipeDownCount);
        //    SingletonManager.Get<GetWaterManager>().CheckIfComplete();
        //}
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

    public void GetWater()
    {

    }
    

}
