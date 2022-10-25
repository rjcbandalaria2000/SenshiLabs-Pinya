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
    public List<Transform> randomPoints = new();
    public GameObject target;

    [Header("Distance")]
    public float distance;
    public float minDistance;
    public float fleeDistance;

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;
    public Coroutine goToTargetRoutine;

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


        randomPoints = new(minigame.points);
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
        //else
        //{

        //    int RNG = Random.Range(0, randomPoints.Count);

        //    speed = defaultSpeed;

        //    target = randomPoints[RNG].transform;

        //    distance = Vector3.Distance(target.transform.position, this.transform.position);
        //    goToTargetRoutine = StartCoroutine(goToTarget());


        //    Debug.Log("Position Set");
        //}

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

    public void updateAITag(AITagMinigame otherChild)
    {
        otherChild.isTag = true;
        this.isTag = false;

        otherChild.spriteUpdate();
        this.spriteUpdate();

        this.setTarget();
        otherChild.setTarget();
       // StartCoroutine(targetCooldown(otherChild));
    }

    public void updatePlayerTag(TagMinigamePlayer otherPlayer)
    {
        otherPlayer.isTag = true;
        this.isTag = false;

        otherPlayer.spriteUpdate();
        this.spriteUpdate();

        this.setTarget();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AITagMinigame>() != null) //other AI
        {
            if (isTag == true && other.GetComponent<AITagMinigame>().isTag == false)
            {
                StopCoroutine(goToTargetRoutine);
                other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
                updateAITag(other.GetComponent<AITagMinigame>());
                Debug.Log("AI Tag");

            }
        }
        else if(other.GetComponent<TagMinigamePlayer>() != null)
        {
            if (isTag == true && other.GetComponent<TagMinigamePlayer>().isTag == false)
            {
                StopCoroutine(goToTargetRoutine);
                updatePlayerTag(other.GetComponent<TagMinigamePlayer>());
                Debug.Log("AI Tag");

            }
        }
    
    }

}
