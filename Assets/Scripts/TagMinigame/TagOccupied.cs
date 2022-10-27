using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagOccupied : MonoBehaviour
{
    public bool isObjectTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AITagMinigame>().isTag == true || other.gameObject.GetComponent<TagMinigamePlayer>().isTag == true)
        {
            isObjectTag = true;
            Debug.Log("TagOccupied");
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        isObjectTag = false;
    }
}
