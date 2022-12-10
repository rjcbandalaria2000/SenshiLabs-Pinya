using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

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
    public int              maxBuckets = 3;

    [Header("UI")]
    public GameObject       UI;

    [Header("Animation")]
    public float            animationDuration = 1f;

    private int             playerSwipeUpCount;
    private int             playerSwipeDownCount;
    private Camera          mainCamera;
    private Vector2         initialMousePosition;

    [Header("Audio")]
    public AudioClip pullDownSFX;
    public AudioClip pullUpSFX;

    private SFXManager sFXManager;

    [Header("Unity Events")]
    public UnityEvent<int, float> OnBucketFilled = new();
 
    // Start is called before the first frame update
    void Start()
    {
        sFXManager = GetComponent<SFXManager>();
        Initialize();
    }

    public void Initialize()
    {
        mainCamera = Camera.main;
        RequiredSwipes = SingletonManager.Get<GetWaterManager>().RequiredNumSwipes;
        //CanSwipeDown = true;
        CanSwipeUp = false;
        SwipedDown = false;
        SwipedUp = false;

        SingletonManager.Get<GetWaterManager>().slider.maxValue = RequiredSwipes;
        SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
        Events.OnBucketUsed.Invoke();
        Events.OnBucketDrop.Invoke();
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
                        SingletonManager.Get<GetWaterManager>().slider.DOValue(playerSwipeDownCount, animationDuration, false);
                        sFXManager.PlaySFX(pullDownSFX);
                        // SingletonManager.Get<GetWaterManager>().slider.value = playerSwipeDownCount;
                        Events.OnObjectiveUpdate.Invoke();
                    }
                    if (playerSwipeDownCount >= RequiredSwipes)
                    {
                        if(fillWaterBucket == null) { return; }
                        fillWaterBucket.StartFillingBucket();
                        Events.OnBucketRetrieve.Invoke();
                       
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
                    OnBucketFilled.Invoke(availableBuckets - 1, fillWaterBucket.GetNormalizedWaterAmount());
                    //waterBuckets.Add(fillWaterBucket.GetNormalizedWaterAmount());
                    //Reset the waterAmount 
                    fillWaterBucket.ResetWaterBucket();
                    //subtract 1 fro the available buckets 
                    availableBuckets--;
                    Events.OnBucketUsed.Invoke();
                    
                    //Reset swipes 
                    playerSwipeDownCount = 0;
                    sFXManager.PlaySFX(pullUpSFX);

                    //Slider for the bucket depth
                    SingletonManager.Get<GetWaterManager>().slider.DOValue(playerSwipeDownCount, animationDuration, false);
                    Events.OnBucketDrop.Invoke();
                }
                if(availableBuckets <= 0)
                {
                    Events.OnBucketUsed.Invoke();
                    Debug.Log("No more buckets");
                    SingletonManager.Get<GetWaterManager>().CheckIfComplete();
                }
                CanSwipeUp = false;
            }
        }
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
