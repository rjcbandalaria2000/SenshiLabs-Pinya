using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHidingChild : MonoBehaviour
{
    [SerializeField] private GameObject child;

    public Event onCoverDisable;

    public HideSeekManager hideSeekManager;
    //public bool isHiding;

    private void Start()
    {
        //isHiding = true;
        child = this.gameObject;    
        hideSeekManager = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
   
    }
    private void Update()
    {
   
    }

    private void OnMouseDown()
    {
        
            Debug.Log("Child Found");
            child.SetActive(false); // or DeleteDestroy?
            hideSeekManager.score += 1;
            hideSeekManager.count -= 1;
            SingletonManager.Get<DisplayChildCount>().updateChildCount();
            hideSeekManager.CheckIfFinished();
          
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("HidingSpot"))
        {
           
            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingSpot"))
        {
           
            this.gameObject.layer = LayerMask.NameToLayer("ChildHide");
        }
    }

}
