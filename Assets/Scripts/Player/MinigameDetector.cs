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

    public void OnTriggerStay2D(Collider2D collision)
    { 
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject){

            //interactedObject.EvtInteracted.Invoke(Parent);
            //EvtInteract.Invoke();
            Events.OnInteract.Invoke(Parent);
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject)
        {
            //interactedObject.EvtFinishInteract.Invoke(Parent);
            //EvtFinishInteract.Invoke();
            Events.OnFinishInteract.Invoke(Parent);
        }
    }
}
