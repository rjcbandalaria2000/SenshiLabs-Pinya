using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<GameObject> EvtInteracted = new();

    public void Interact(GameObject player = null)
    {
        EvtInteracted.Invoke(player);
    }

    
}
