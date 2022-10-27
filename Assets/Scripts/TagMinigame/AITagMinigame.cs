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
  
        }
        else
        {
            defaultState.SetActive(true);
            tagState.SetActive(false);

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
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                distance = Vector2.Distance(this.transform.position, target.transform.position);

            }
            
        }
        else
        {
            setTarget();
            Debug.Log("New Target");
        }
    }

    IEnumerator goToTarget()
    {
        yield return new WaitForSeconds(delaySpeed);

        while (distance > minDistance)
        {
            if(target != null)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
                distance = Vector2.Distance(this.transform.position, target.transform.position);
                
            }
            yield return null;
        }

        yield return new WaitForSeconds(delaySpeed);
        setTarget();
        Debug.Log("New Target");

        yield return null;
    }


   
  
}
    