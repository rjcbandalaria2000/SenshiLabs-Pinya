using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHidingChild : MonoBehaviour
{
    [SerializeField] private GameObject child;

    public HideSeekManager hideSeekManager;
    [SerializeField] private bool isHiding;

    private void Start()
    {
        isHiding = true;
        child = this.gameObject;    
        hideSeekManager = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
    }
    private void Update()
    {
   
    }

    private void OnMouseDown()
    {
        if(isHiding == false)
        {
            Debug.Log("Child Found");
            child.SetActive(false); // or DeleteDestroy?
            hideSeekManager.score += 1;
            hideSeekManager.count -= 1;
            SingletonManager.Get<DisplayChildCount>().updateChildCount();
            hideSeekManager.CheckIfFinished();
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("HidingSpot"))
        {
            isHiding = true;
            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HidingSpot"))
        {
            isHiding = false;
            this.gameObject.layer = LayerMask.NameToLayer("ChildHide");
        }
    }
}
