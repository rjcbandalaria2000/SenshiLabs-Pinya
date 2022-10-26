using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITagMinigame : MonoBehaviour
{
    public int ID;
    public bool isTag;
    //public bool isRoam;

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
    public float changeTimer;

    [Header("Target")]
    public List<GameObject> potentialTargets = new();
    public List<Transform> randomPoints;
    public GameObject target;

    [Header("Distance")]
    public float distance;
    public float minDistance;
 

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;
    public Coroutine goToTargetRoutine;
    public Coroutine collideRoutine;

    [Header("Tag Routines")]
    public Coroutine AITagAIRoutine;
    public Coroutine AITagPlayerRoutine;
    public Coroutine PlayerTagAIRoutine;

    public TagManager minigame;

    //public GameObject previousTag;

    private void Start()
    {
      //  previousTag = null;

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
        if (this.isTag)
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
        if (this.isTag == true)
        {
            int RNG = Random.Range(0, potentialTargets.Count);
            this.target = potentialTargets[RNG];

            this.speed = taggedSpeed;
            distance = Vector3.Distance(this.target.transform.position, this.transform.position);
            goToTargetRoutine = StartCoroutine(goToTarget());

            //if (this.target == previousTag)
            //{
            //    this.target = null;
            //    setTarget();
            //    Debug.Log("Refresh Target");
            //}
            //else
            //{
            //    this.speed = taggedSpeed;
            //    distance = Vector3.Distance(this.target.transform.position, this.transform.position);
            //    goToTargetRoutine = StartCoroutine(goToTarget());
            //}

        }
        else
        {

            int RNG = Random.Range(0, randomPoints.Count);
            if(randomPoints[RNG].gameObject.GetComponent<TagOccupied>().isObjectTag == true) 
            {
                setTarget();
                Debug.Log("Avoid Target");
            }
            else
            {
                this.speed = defaultSpeed;
                this.target = randomPoints[RNG].gameObject;

                distance = Vector3.Distance(this.target.transform.position, this.transform.position);
                goToTargetRoutine = StartCoroutine(goToTarget());
            }
         
        }

    }

    public IEnumerator goToTarget()
    {

        while (distance > minDistance)
        {
            if(target != null)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, this.target.transform.position, this.speed * Time.deltaTime);
                distance = Vector3.Distance(this.target.transform.position, this.transform.position);
               
            }
            yield return null;
        }
        
        yield return new WaitForSeconds(changeTimer);
        
        if (distance <= minDistance)
        {
            this.target = null;
            setTarget();

            Debug.Log("NewTarget");
        }

      
    }

    public IEnumerator AiTagAi(AITagMinigame otherChild)
    {
        //this.isTag = false;
        //this.spriteUpdate();
        this.setTarget();

        //otherChild.isTag = true;
        //otherChild.spriteUpdate();
       
        yield return new WaitForSeconds(delaySpeed);

     
        otherChild.setTarget();

       
       
        //StartCoroutine(coolDown(otherChild));

    }

    public IEnumerator AiTagPlayer(TagMinigamePlayer otherPlayer)
    {
        this.isTag = false;
        this.spriteUpdate();
        this.setTarget();

        otherPlayer.isTag = true;
        otherPlayer.spriteUpdate();
        
        yield return new WaitForSeconds(delaySpeed);

       
        

    }

    public IEnumerator playerTagAI(TagMinigamePlayer otherPlayer)
    {
        otherPlayer.isTag = false;
        otherPlayer.spriteUpdate();

        this.isTag = true;
        this.spriteUpdate();
        
        yield return new WaitForSeconds(delaySpeed);

        this.setTarget();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // collideRoutine = StartCoroutine(collide(other));

        if (other.GetComponent<AITagMinigame>() != null) //AI tag AI
        {
            if (this.isTag == true && other.GetComponent<AITagMinigame>().isTag == false)
            {
                StopCoroutine(this.goToTargetRoutine);
                this.target = null;

                this.isTag = false;
                other.GetComponent<AITagMinigame>().isTag = true;

                this.spriteUpdate();
                other.GetComponent<AITagMinigame>().spriteUpdate();

               
                //StopCoroutine(this.goToTargetRoutine);
                //this.target = null;

                AITagAIRoutine = StartCoroutine(AiTagAi(other.GetComponent<AITagMinigame>()));

                Debug.Log(this.gameObject + "Collide: " + other.gameObject);


            }
        }
        else if (other.GetComponent<TagMinigamePlayer>() != null)
        {
            if (this.isTag == true && other.GetComponent<TagMinigamePlayer>().isTag == false) //AI tag Player
            {
                this.target = null;
                AITagPlayerRoutine = StartCoroutine(AiTagPlayer(other.GetComponent<TagMinigamePlayer>()));
                Debug.Log(this.gameObject + " tag " + other.gameObject);

            }
            else if (this.isTag == false && other.GetComponent<TagMinigamePlayer>().isTag == true) // player tag AI
            {
                StopCoroutine(this.goToTargetRoutine);
                this.target = null;

                PlayerTagAIRoutine = StartCoroutine(playerTagAI(other.GetComponent<TagMinigamePlayer>()));
                Debug.Log("player tag " + other.gameObject);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        AITagAIRoutine = null;
        AITagPlayerRoutine = null;
        PlayerTagAIRoutine = null;
    }


    public IEnumerator collide(Collider2D other)
    {
        if (other.GetComponent<AITagMinigame>() != null) //AI tag AI
        {
            if (this.isTag == true )
            {
                StopCoroutine(this.goToTargetRoutine);
                this.target = null;
           
                AITagAIRoutine = StartCoroutine(AiTagAi(other.GetComponent<AITagMinigame>()));

                Debug.Log(this.gameObject + "Collide: " + other.gameObject);

 
            }
            else
            {
                yield return null;
            }
        }
        else if (other.GetComponent<TagMinigamePlayer>() != null) 
        {
            if (this.isTag == true && other.GetComponent<TagMinigamePlayer>().isTag == false) //AI tag Player
            {
                
                this.target = null;


                AITagPlayerRoutine = StartCoroutine(AiTagPlayer(other.GetComponent<TagMinigamePlayer>()));
                Debug.Log(this.gameObject +" tag " + other.gameObject);
                yield return null;

            }
            else if (this.isTag == false && other.GetComponent<TagMinigamePlayer>().isTag == true) // player tag AI
            {
                StopCoroutine(this.goToTargetRoutine);
                this.target = null;


                PlayerTagAIRoutine = StartCoroutine(playerTagAI(other.GetComponent<TagMinigamePlayer>()));
                Debug.Log("player tag " + other.gameObject);
                yield return null;


        
            }
            else
            {
                yield return null;
            }
        }
    }

}


//if (this.target != previousTag)
//{
//    StopCoroutine(this.goToTargetRoutine);
//    this.target = null;
//    other.GetComponent<AITagMinigame>().target = null;

//    StartCoroutine(tagImmune(other));

//    //other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
//    this.previousTag = null;
//    AITagAIRoutine = StartCoroutine(AiTagAi(other.GetComponent<AITagMinigame>()));

//    Debug.Log(this.gameObject + "Collide: " + other.gameObject);

//}
//else
//{
//    yield return null;
//}

//if (this.gameObject != other.GetComponent<TagMinigamePlayer>().previousTag)
//{
//    StopCoroutine(this.goToTargetRoutine);
//    this.target = null;

//    other.GetComponent<TagMinigamePlayer>().previousTag = null;
//  //  this.previousTag = other.gameObject;

//    PlayerTagAIRourine = StartCoroutine(playerTagAI(other.GetComponent<TagMinigamePlayer>()));
//    Debug.Log("player tag AI");
//    yield return null;
//}

//if (this.isRoam == false)
//{
//    collideRoutine = StartCoroutine(collide(other));
//}
//else if(other.gameObject.GetComponent<TagMinigamePlayer>() != null)
//{
//    collideRoutine = StartCoroutine(collide(other));
//}

//public IEnumerator tagImmune(Collider2D other)
// {
//     other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
//     yield return new WaitForSeconds(3f);
//     other.GetComponent<AITagMinigame>().previousTag = null;
// }

//other.GetComponent<AITagMinigame>().target = null;

//StartCoroutine(tagImmune(other));

//other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
//this.previousTag = null;