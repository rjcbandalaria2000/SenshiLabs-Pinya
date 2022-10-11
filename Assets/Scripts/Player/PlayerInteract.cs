using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Interactable InteractableObject;

    private Coroutine interactRoutine;
    // Start is called before the first frame update
    void Start()
    {
        if(interactRoutine != null)
        {
            StopCoroutine(interactRoutine);
        }
        interactRoutine = StartCoroutine(Interact());
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    IEnumerator Interact()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (InteractableObject)
                {
                    InteractableObject.Interact(this.gameObject);
                }
            }
            
            yield return null;
        }
        
    }

    public void OnSceneChange()
    {
        //Remove any listener if there is still an interactable object 
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        if (InteractableObject == null) { return; }
        MinigameObject minigameObject = InteractableObject.gameObject.GetComponent<MinigameObject>();
        if (minigameObject == null) { return; }
        Events.OnInteract.RemoveListener(minigameObject.Interact);
        Events.OnFinishInteract.RemoveListener(minigameObject.EndInteract);
        InteractableObject = null;
    }

   
}
