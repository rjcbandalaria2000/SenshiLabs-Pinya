using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    public Interactable     Interactable;
    public bool             isInteracted;

    public void Start()
    {
        if( Interactable == null)
        {
            Interactable = this.GetComponent<Interactable>();
        }
        if (Interactable)
        {
            //Listen to the interactable event and proceed to interact with the player
            Interactable.EvtInteracted.AddListener(Interact);
            Interactable.EvtFinishInteract.AddListener(EndInteract);
        }
    }

    public void Interact(GameObject player = null)
    {
        Debug.Log("Interacted with " + player.name);
    }

    public void EndInteract(GameObject player = null)
    {
        Debug.Log("End Interact");
    }
}
