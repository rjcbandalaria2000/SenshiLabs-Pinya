using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Header("States")]
    public bool swipedRight;
    public bool swipedLeft;
    public bool IsClean = false;

    [Header("Values")]
    public int SwipeRequired;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float SwipeLeftAccept = -0.5f;
    [Range(0f, 1f)]
    public float SwipeRightAccept = 0.5f;

    [Header("Models")]
    public GameObject CleanPlateModel;
    public GameObject DirtyPlateModel;
   

    private int swipeCounter;
    private Vector2 initialPosition;

    private Sponge sponge;
    private Coroutine spongeInteractRoutine;

    // Start is called before the first frame update
    void Start()
    {
        spongeInteractRoutine = null;
        ChangeModel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sponge collidedSponge = collision.gameObject.GetComponent<Sponge>();
        if (collidedSponge)
        {
            sponge = collidedSponge;
            initialPosition = sponge.gameObject.transform.position;
            StartSpongeInteract();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Sponge collidedSponge = collision.gameObject.GetComponent<Sponge>();
        if (collidedSponge)
        {
            sponge = null;
            StopSpongeInteract();
        }
    }

    public void StartSpongeInteract()
    {
        spongeInteractRoutine = StartCoroutine(SpongeInteract());
    }

    public void StopSpongeInteract()
    {
        StopCoroutine(spongeInteractRoutine);
    }

    public void ChangeModel()
    {
        if (IsClean)
        {
            CleanPlateModel.SetActive(true);
            DirtyPlateModel.SetActive(false);
        }
        else
        {
            CleanPlateModel.SetActive(false);
            DirtyPlateModel.SetActive(true);
        }
    }

    IEnumerator SpongeInteract()
    {

        while (true)
        {
            if (sponge == null) { break; }
            Vector2 spongePosition = sponge.transform.position + this.gameObject.transform.position;//sponge.gameObject.transform.position - (Vector3)initialPosition;

            if (spongePosition.normalized.x < SwipeLeftAccept) // if sponge swiped left 
            {

                swipedLeft = true;
            }
            if (spongePosition.normalized.x > SwipeRightAccept) // if sponge swiped right
            {

                swipedRight = true;

            }
            if (swipedRight && swipedLeft) // if the player both reached both ends
            {
                swipeCounter++;
                swipedLeft = false;
                swipedRight = false;
            }
            if (swipeCounter >= SwipeRequired)
            {
                IsClean = true;
                ChangeModel();

            }
            Debug.Log("XCoordinates: " + spongePosition.normalized.x);

            yield return null;
        }
        
        
    }

  
}
