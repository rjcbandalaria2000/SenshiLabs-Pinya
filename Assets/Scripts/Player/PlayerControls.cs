using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public int      MoveSpeed = 5;
    public Animator animator;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = this.transform.position;

      
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if (EventSystem.current.IsPointerOverGameObject()) { return; }
            animator.SetBool("IsIdle", false);
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = this.transform.position.z;
        }
        
        
    }

    private void FixedUpdate()
    {
        //Avoids jittering 
        if(Vector3.Distance(this.transform.position,targetPosition) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);
            
        }
        else
        {
            animator.SetBool("IsIdle", true);
        }
    }
}
