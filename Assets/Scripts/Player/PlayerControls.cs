using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    [Header("Setup Values")]
    public int          moveSpeed = 5;
    public Animator     animator;

    //For model flipping 
    [Header("Character Model")]
    public Transform    playerModel;
    public float        flippedAngle = 180;
    public float        normalAngle = 0;

    private Vector3     targetPosition;
    private PlayerData  playerData;
    private Camera      mainCamera;


    private StepSFX stepSFX;
    void Start()
    {
        if (!this.enabled)
        {
            this.enabled = true;
        }
        Initialize();
        
    }

    public void Initialize()
    {
        mainCamera = Camera.main;

        if (playerData == null)
        {
            playerData = SingletonManager.Get<PlayerData>();
        }
        RestoreLastPlayerPosition();
        stepSFX = gameObject.GetComponent<StepSFX>();
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
            animator.SetBool("IsIdle", false);
            targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = this.transform.position.z;


            // for model flipping based on where the player is going 
           
            Vector2 lookDirection = targetPosition - this.transform.position; 
            if(lookDirection.normalized.x > 0)
            {
                //the target position is going right
                playerModel.rotation = Quaternion.Euler(0, flippedAngle, 0);
            }
            else if(lookDirection.normalized.x < 0)
            {
                playerModel.rotation = Quaternion.Euler(0, normalAngle, 0);
            }
        }
        
        
    }

    private void FixedUpdate()
    {
        //Avoids jittering 
        if(Vector3.Distance(this.transform.position,targetPosition) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //stepSFX.Step();
        }
        else
        {
            animator.SetBool("IsIdle", true);
        }
    }

    public void RestoreLastPlayerPosition()
    {
        if (playerData)
        {
            if (playerData.hasSaved)
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
        else // if there is no player data present 
        {
            targetPosition = this.transform.position;
        }
    }

}
