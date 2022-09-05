using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    public Interactable Interactable;

    public void Start()
    {
        if( Interactable == null)
        {
            Interactable = this.GetComponent<Interactable>();
        }
        if (Interactable)
        {
            Interactable.EvtInteracted.AddListener(Interact);
        }
    }

    public void Interact(GameObject player = null)
    {
        Debug.Log("Interacted with " + player.name);
    }
}
