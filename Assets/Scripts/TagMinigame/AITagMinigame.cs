using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITagMinigame : MonoBehaviour
{
    public int ID;
    public bool isTag;

    //[Header("Tag")]
    //public GameObject tagCollider;

    [Header("States")]
    public GameObject defaultState;
    public GameObject tagState;

    [Header("Speed")]
    public float taggedSpeed;
    public float defaultSpeed;
    public float speed;
    public float delaySpeed;

    [Header("Target")]
    public List<GameObject> potentialTargets = new();
    public List<Transform> randomPoints;
    public GameObject target;

    [Header("Distance")]
    public float distance;
    public float minDistance;
    public float fleeDistance;

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;
    public Coroutine goToTargetRoutine;
    public Coroutine collideRoutine;

    [Header("Tag Routines")]
    public Coroutine AITagAIRoutine;
    public Coroutine AITagPlayerRoutine;
    public Coroutine PlayerTagAIRourine;

    public TagManager minigame;

    public GameObject previousTag;

    private void Start()
    {
        previousTag = null;


        minDistance = 1;

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
        if (isTag == true)
        {
            int RNG = Random.Range(0, potentialTargets.Count);

            speed = taggedSpeed;

            target = potentialTargets[RNG];

            if (target == previousTag)
            {
                setTarget();
                Debug.Log("Refresh Target");
            }
            else
            {
                distance = Vector3.Distance(target.transform.position, this.transform.position);
                goToTargetRoutine = StartCoroutine(goToTarget());
            }

        }
        else
        {

            int RNG = Random.Range(0, randomPoints.Count);

            speed = defaultSpeed;
            if(randomPoints[RNG].gameObject.GetComponent<TagOccupied>().isObjectTag == true) 
            {
                setTarget();
                Debug.Log("Avoid Target");
            }
            else
            {
                target = randomPoints[RNG].gameObject;

                distance = Vector3.Distance(target.transform.position, this.transform.position);
                goToTargetRoutine = StartCoroutine(goToTarget());
            }
         
        }

    }

    public IEnumerator goToTarget()
    {

        while (distance > minDistance)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, target.transform.position, speed * Time.deltaTime);
            distance = Vector3.Distance(target.transform.position, this.transform.position);
            yield return null;
        }
        
        yield return new WaitForSeconds(delaySpeed);
       
        if (isTag == false)
        {
            if (distance < minDistance)
            {
                setTarget();
                Debug.Log("NewTarget");
            }
        }
        else
        {
            yield return null;
        }
    }

    public IEnumerator AiTagAi(AITagMinigame otherChild)
    {
        otherChild.isTag = true;
        this.isTag = false;

        otherChild.spriteUpdate();
        this.spriteUpdate();

        this.setTarget();

        yield return new WaitForSeconds(1f);

        otherChild.setTarget();
        //StartCoroutine(coolDown(otherChild));
       
    }

    public void updatePlayerTag(TagMinigamePlayer otherPlayer)
    {
        otherPlayer.isTag = true;
        this.isTag = false;

        otherPlayer.spriteUpdate();
        this.spriteUpdate();

        this.setTarget();

    }

    public IEnumerator playerTagAI(TagMinigamePlayer otherPlayer)
    {
        otherPlayer.isTag = false;
        this.isTag = true;

        otherPlayer.spriteUpdate();
        this.spriteUpdate();

        yield return new WaitForSeconds(1f);

        this.setTarget();
    }

    IEnumerator coolDown(AITagMinigame otherAI)
    {
        otherAI.speed = 0;
        yield return new WaitForSeconds(1.5f);
        otherAI.speed = otherAI.taggedSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collideRoutine = StartCoroutine(collide(other));
    
    }

    IEnumerator collide(Collider2D other)
    {
        if (other.GetComponent<AITagMinigame>() != null) //AI tag AI
        {
            if (isTag == true && other.GetComponent<AITagMinigame>().isTag == false)
            {
               
                other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
                this.previousTag = null;
                AITagAIRoutine = StartCoroutine(AiTagAi(other.GetComponent<AITagMinigame>()));
               
                Debug.Log("Other AI Tag");

            }
        }
        else if (other.GetComponent<TagMinigamePlayer>() != null) 
        {
            if (isTag == true && other.GetComponent<TagMinigamePlayer>().isTag == false) //AI tag Player
            {
                //StopCoroutine(goToTargetRoutine);
                other.GetComponent<TagMinigamePlayer>().previousTag = this.gameObject;
                updatePlayerTag(other.GetComponent<TagMinigamePlayer>());
                Debug.Log("Ai Tag Player");

            }
            else if (isTag == false && other.GetComponent<TagMinigamePlayer>().isTag == true) // player tag AI
            {
                if(this.gameObject != other.GetComponent<TagMinigamePlayer>().previousTag)
                {
                    other.GetComponent<TagMinigamePlayer>().previousTag = null;
                    this.previousTag = other.gameObject;
                    PlayerTagAIRourine = StartCoroutine(playerTagAI(other.GetComponent<TagMinigamePlayer>()));
                    Debug.Log("player tag AI");
                }
        
            }
        }

        yield return null;
    }

}
