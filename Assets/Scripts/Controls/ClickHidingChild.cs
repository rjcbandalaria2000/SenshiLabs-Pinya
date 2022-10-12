using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHidingChild : MonoBehaviour
{
    [SerializeField] private GameObject child;

    public HideSeekManager hideSeekManager;

    private void Start()
    {
        child = this.gameObject;    
        hideSeekManager = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Child Found");
        hideSeekManager.score += 1;
        hideSeekManager.count -= 1;
        SingletonManager.Get<DisplayChildCount>().updateChildCount();
        hideSeekManager.CheckIfFinished();
        child.SetActive(false); // or DeleteDestroy?
       

        

    }
}
