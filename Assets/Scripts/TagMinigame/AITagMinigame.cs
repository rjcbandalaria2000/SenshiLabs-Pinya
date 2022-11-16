using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITagMinigame : MonoBehaviour
{
    public int ID;
    public bool isTag;

    public GameObject tagCollider;

    [Header("States")]
    public GameObject defaultState;
    public GameObject tagState;

    [Header("Speed")]
    public float taggedSpeed;
    public float defaultSpeed;
    public float speed;
    public float delaySpeed;
    public float stopTimer;

    [Header("Target")]
    public List<GameObject> potentialTargets = new();
    public List<Transform> randomPoints;
    public GameObject target;

    [Header("Distance")]
    public float distance;
    public float minDistance;

    [Header("Coroutines")]
    public Coroutine goToTargetRoutine;

    public TagManager minigame;

    public Animator animator;

    [Header("Character Model")]
    public float flippedAngle = 180;
    public float normalAngle = 0;

    private void Start()
    {
        spriteUpdate();

        if (minigame == null)
        {
            if (GameObject.FindObjectOfType<TagManager>() != null)
            {
                minigame = GameObject.FindObjectOfType<TagManager>().GetComponent<TagManager>();
            }
        }

        randomPoints = minigame.points;
        potentialTargets = new(minigame.activeAI);

        for (int i = 0; i < potentialTargets.Count; i++)
        {
            if (ID == potentialTargets[i].GetComponent<AITagMinigame>().ID)
            {
                potentialTargets.RemoveAt(i);
            }
        }
       potentialTargets.Add(minigame.spawnedPlayer.gameObject);

        setTarget();

       

    }

    private void FixedUpdate()
    {
        moveToTarget();
    }

    public void spriteUpdate()
    {
        if (isTag)
        {
            defaultState.SetActive(false);
            tagState.SetActive(true);

            animator = tagState.GetComponentInChildren<Animator>();
  
        }
        else
        {
            defaultState.SetActive(true);
            tagState.SetActive(false);

            animator = defaultState.GetComponentInChildren<Animator>();
        }
    }

    public void setTarget()
    {
        if(this.isTag == true)
        {
            speed = taggedSpeed;
            int RNG = Random.Range(0, potentialTargets.Count);
            target = potentialTargets[RNG];
            distance = Vector2.Distance(this.transform.position, target.transform.position);

            //goToTargetRoutine = StartCoroutine(goToTarget());
           
           
        }
        else
        {
            speed = defaultSpeed;
            int RNG = Random.Range(0, randomPoints.Count);
            target = randomPoints[RNG].gameObject;
            distance = Vector2.Distance(this.transform.position, target.transform.position);

            //goToTargetRoutine = StartCoroutine(goToTarget());
 
        }
    }

    public void moveToTarget()
    {
        if (distance > minDistance)
        {
            if (target != null)
            {
                animator.SetBool("IsIdle", false);
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                distance = Vector2.Distance(this.transform.position, target.transform.position);

                Vector2 lookDirection = target.transform.position - this.transform.position;
                if (lookDirection.normalized.x > 0)
                {
                   
                    if (defaultState.activeSelf == true)
                    {
                        defaultState.transform.rotation = Quaternion.Euler(0, flippedAngle, 0);
                    }
                    else if (tagState.activeSelf == true)
                    {
                        tagState.transform.rotation = Quaternion.Euler(0, flippedAngle, 0);
                    }


                }
                else if (lookDirection.normalized.x < 0)
                {
                    if (defaultState.activeSelf == true)
                    {
                        defaultState.transform.rotation = Quaternion.Euler(0, normalAngle, 0);
                    }
                    else if (tagState.activeSelf == true)
                    {
                        tagState.transform.rotation = Quaternion.Euler(0, normalAngle, 0);
                    }
                }

            }
            
        }
        else
        {
          
            setTarget();
            Debug.Log("New Target");
        }
    }

   
  
}
    