using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //public UnityEvent<GameObject>   EvtInteracted = new();
    //public UnityEvent<GameObject> EvtFinishInteract = new();

    public void Interact(GameObject player = null)
    {
        //EvtInteracted.Invoke(player);
        Events.OnInteract.Invoke(player);
    }

    public void FinishInteract (GameObject player = null)
    {
        //EvtFinishInteract.Invoke(player);   
        Events.OnFinishInteract.Invoke(player);
    }

    
}
