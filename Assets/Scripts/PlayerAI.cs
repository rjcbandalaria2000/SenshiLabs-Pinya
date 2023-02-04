using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [Header("Setup Values")]
    public int moveSpeed = 5;
    public Animator animator;

    //For model flipping 
    [Header("Character Model")]
    public Transform playerModel;
    public float flippedAngle = 180;
    public float normalAngle = 0;

    private Vector3 targetPosition;
    private PlayerData playerData;
    private Camera mainCamera;

    public List<GameObject> wayPoints;

    public AIDestinationSetter AiSetter;

    private StepSFX stepSFX;

    Vector2 lastPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        if (!this.enabled)
        {
            this.enabled = true;
            
        }
        AiSetter = this.GetComponent<AIDestinationSetter>();
        AiSetter.target = null;
        AiSetter.enabled = false;
        animator.SetBool("IsIdle", true);
        Initialize();

    }

    public void Initialize()
    {
        mainCamera = Camera.main;
        AiSetter = GetComponent<AIDestinationSetter>();
        if (playerData == null)
        {
            playerData = SingletonManager.Get<PlayerData>();
        }
        RestoreLastPlayerPosition();
        stepSFX = gameObject.GetComponent<StepSFX>();
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

    // Update is called once per frame
    void Update()
    {
      
      
    }
    private void FixedUpdate()
    {
       
    }


   public IEnumerator goToTargetRoutine(int index)
    {
        Vector2 targetPos = wayPoints[index].transform.position;
        AiSetter.target = wayPoints[index].transform;

        lastPlayerPos = Vector2.zero;
        while (Vector2.Distance(this.transform.position,AiSetter.target.position) > 1)
        {
            //lastPlayerPos = this.transform.position;
            if(lastPlayerPos != null || lastPlayerPos != Vector2.zero)
            {
                //targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                //targetPosition.z = this.transform.position.z;

                // for model flipping based on where the player is going 

                // Vector2 lookDirection = targetPos - (Vector2)this.transform.position;
                Vector2 lookDirection = (Vector2)this.transform.position - lastPlayerPos;

                // Vector2 lookDirection2 = 

                if (lookDirection.normalized.x > 0)
                {
                    //the target position is going right    
                    playerModel.rotation = Quaternion.Euler(0, flippedAngle, 0);
                }
                else if (lookDirection.normalized.x < 0)
                {
                    playerModel.rotation = Quaternion.Euler(0, normalAngle, 0);
                }

                animator.SetBool("IsIdle", false);
            }

            //  transform.position = Vector2.Lerp(this.transform.position, wayPoints[index].transform.position, moveSpeed * Time.deltaTime);
            lastPlayerPos = this.transform.position;

            yield return new WaitForSeconds(0.15f);

          
            //stepSFX.Step();
        }

        yield return new WaitForSeconds(0.5f);
        AiSetter.target = null;
        animator.SetBool("IsIdle", true);
      
       
    }
}
