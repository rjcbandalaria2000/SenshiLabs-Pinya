using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Leaves : MonoBehaviour
{
    [Header("States")]
    public bool swipedRight;
    public bool swipedLeft;

    [Header("Values")]
    public int SwipeRequired;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float SwipeLeftAccept = -0.5f;
    [Range(0f, 1f)]
    public float SwipeRightAccept = 0.5f;

    private Camera mainCamera;
    private int swipeCounter;
    private Vector2 initialPosition;
    public GameObject child;

    AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        swipedRight = false;
        swipedLeft = false;
        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
       

    }

    private void OnMouseDown()
    {
        initialPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        //can be improved to be transfered in Sweeping Controls
        //Get mouse position
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)initialPosition;
        if (mousePosition.normalized.x < SwipeLeftAccept)
        {
            // if the mouse moved to the left
            swipedLeft = true;
        }
        if (mousePosition.normalized.x > SwipeRightAccept)
        {
            // if the mouse moved to the right 
            swipedRight = true;

        }
        if (swipedRight && swipedLeft)
        {
            audioSource.PlayOneShot(audioClip);
            swipeCounter++;
            swipedLeft = false;
            swipedRight = false;
        }
        if (swipeCounter >= SwipeRequired)
        {
           
            this.gameObject.SetActive(false);
        }
        //Debug.Log("X coordinate: " + mousePosition.normalized.x);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Collide_Trigger");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.layer = LayerMask.NameToLayer("ChildHide");
    }
}
