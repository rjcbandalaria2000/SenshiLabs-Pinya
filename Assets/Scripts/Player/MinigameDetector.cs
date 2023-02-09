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
    private MinigameObject interactedMinigame;
    public DisplayInteractMessage interactMessage;
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
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Only listen when collided with a minigame object
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject)
        {
            Events.OnEnterInteraction.Invoke();

            PlayerInteract playerInteract = Parent.GetComponent<PlayerInteract>();
            if (playerInteract == null) { return; }
            playerInteract.InteractableObject = interactedObject;
            
            MinigameObject detectedMinigame = collision.gameObject.GetComponent<MinigameObject>();
            if (detectedMinigame)
            {
                interactedMinigame = detectedMinigame;//Save interacted object
                Events.OnInteract.AddListener(detectedMinigame.Interact);
                Events.OnFinishInteract.AddListener(detectedMinigame.EndInteract);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("Colliding");
    //    Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
    //    if (interactedObject)
    //    {
    //        Events.OnEnterInteraction.Invoke();

    //    }

    //}
    public void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject)
        {
            //Put the interactable object in the player interact controls
            PlayerInteract playerInteract = Parent.GetComponent<PlayerInteract>();
            if (playerInteract == null) { return; }
            playerInteract.InteractableObject = null;
            interactedMinigame = null; //Remove saved interacted object 
            //Method 1 for different spawning scene 
            // Only add listener when collided 
            //Remove all listener when outside of collider 
            MinigameObject detectedMinigame = collision.gameObject.GetComponent<MinigameObject>();
            if (detectedMinigame)
            {
                interactedObject.FinishInteract(Parent);
                Events.OnInteract.RemoveListener(detectedMinigame.Interact);
                Events.OnFinishInteract.RemoveListener(detectedMinigame.EndInteract);
            }
        }
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        if (interactedMinigame)
        {
            Events.OnInteract.RemoveListener(interactedMinigame.Interact);
            Events.OnFinishInteract.RemoveListener(interactedMinigame.EndInteract);
        }
        interactedMinigame = null;
    }
}
