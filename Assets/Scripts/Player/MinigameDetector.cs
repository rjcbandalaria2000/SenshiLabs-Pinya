using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MinigameDetector : MonoBehaviour
{
    public GameObject   Parent;

    //Can still change where this event is placed, used for UI pop up for the mean time 
    public UnityEvent   EvtInteract = new();
    public UnityEvent   EvtFinishInteract = new();

    //private Interactable interactable;
    //private GameObject detectedObject; 

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if(Parent == null)
        {
            Parent = this.transform.parent.gameObject.GetComponent<UnitInfo>().GetParent();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Only listen when collided with a minigame object
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject)
        {
            MinigameObject detectedMinigame = collision.gameObject.GetComponent<MinigameObject>();
            if (detectedMinigame)
            {
                Events.OnInteract.AddListener(detectedMinigame.Interact);
                Events.OnFinishInteract.AddListener(detectedMinigame.EndInteract);
                interactedObject.Interact();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    { 
        //Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        //if (interactedObject){

        //    //Method 1 for different spawning scene 
        //    // Only add listener when collided 
        //    MinigameObject detectedMinigame = collision.gameObject.GetComponent<MinigameObject>();
        //    if (detectedMinigame)
        //    {
        //        Debug.Log("Detected Minigame" + detectedMinigame.name);
        //        interactedObject.Interact();
        //    }
        //}
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject)
        {

            //Method 1 for different spawning scene 
            // Only add listener when collided 
            //Remove all listener when outside of collider 
            MinigameObject detectedMinigame = collision.gameObject.GetComponent<MinigameObject>();
            if (detectedMinigame)
            {
                //Events.OnFinishInteract.AddListener(detectedMinigame.EndInteract);
                interactedObject.FinishInteract();
                Events.OnInteract.RemoveListener(detectedMinigame.Interact);
                Events.OnFinishInteract.RemoveListener(detectedMinigame.EndInteract);
            }
        }
    }
}
