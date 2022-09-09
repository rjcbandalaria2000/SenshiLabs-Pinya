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
    }

    IEnumerator Interact()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (InteractableObject)
                {
                    InteractableObject.Interact(this.gameObject);
                }
            }
            
            yield return null;
        }
        
    }

   
}
