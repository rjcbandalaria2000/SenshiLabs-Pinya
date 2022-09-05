using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameDetector : MonoBehaviour
{
    public GameObject Parent;

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
    { Interactable interactedObject = collision.gameObject.GetComponent<Interactable>();
        if (interactedObject){
            
            interactedObject.EvtInteracted.Invoke(Parent);
        }
        
    }
}
