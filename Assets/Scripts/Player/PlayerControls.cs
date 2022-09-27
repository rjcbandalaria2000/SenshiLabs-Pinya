using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public int          MoveSpeed = 5;
    public Animator     animator;

    private Vector3     targetPosition;
    private PlayerData  playerData;

    void Start()
    {
        if (!this.enabled)
        {
            this.enabled = true;
        }
        playerData = SingletonManager.Get<PlayerData>();
        if (playerData)
        {
            if (playerData.HasSaved)
            {
                // load last location of the player
                this.transform.position = playerData.playerLocation;
                targetPosition = playerData.playerLocation;
                Debug.Log("Going to last location");
            }
            else
            {
                // if no saved position, target will be where the player is 
                targetPosition = this.transform.position;
            }
        }
        else
        {
            targetPosition = this.transform.position;
        }
       
       
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
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
