using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenTag : MonoBehaviour
{

    public int ID;
    public bool isTag;
  
    //public int tagCD;

    [Header("Bounds")]
    //public List<GameObject> points;
    public BoxCollider2D bound;
    private Vector2 size;
    private Vector2 center;

    [Header("Tag")]
    public GameObject tagCollider;

    [Header("States")]
    public GameObject defaultState;
    public GameObject tagState;

    [Header("Location")]
    //public Vector2 startPos;
    //public Vector2 targetPos;
    public float taggedSpeed;
    public float defaultSpeed;
    public float speed;
    public float delaySpeed;

    [Header("Target")]
    public List<GameObject> potentialTargets = new();
    public List<Transform> randomPoints = new();
    public Transform target;

    [Header("Distance")]
    public float distance;
    public float minDistance;
    public float fleeDistance;

    //public SpriteRenderer renderer;

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;
    public Coroutine goToTargetRoutine;

    public TagMiniGameManager minigame;

    public GameObject previousTag;
    // Start is called before the first frame update
    void Start()
    {
       
      // renderer = this.GetComponent<SpriteRenderer>();
        previousTag = null;


        minDistance = 1;

        spriteUpdate();
        //if (renderer != null)
        //{
        //    spriteUpdate();
        //}


        if(minigame == null)
        {
            if(GameObject.FindObjectOfType<TagMiniGameManager>() != null)
            {
                minigame = GameObject.FindObjectOfType<TagMiniGameManager>().GetComponent<TagMiniGameManager>();
            }
        }

        randomPoints = new(minigame.botRandomPos);
        potentialTargets = new(minigame.activeBots);
       
        for (int i = 0; i < potentialTargets.Count; i++)
        {
            if (ID == potentialTargets[i].GetComponent<ChildrenTag>().ID)
            {
                potentialTargets.RemoveAt(i);
            }
        }
        potentialTargets.Add(minigame.spawnPlayer.gameObject);

    }

    //public Vector2 RNG_Position()
    //{
    //    Transform boxBound = bound.GetComponent<Transform>();
    //    center = boxBound.position;
    //    size.x = boxBound.localScale.x * bound.size.x;
    //    size.y = boxBound.localScale.y * bound.size.y;


    //    Vector2 randomPos = new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
    //    return center + randomPos;
    //}
    public void spriteUpdate()
    {
        if (isTag)
        {
           defaultState.SetActive(false);
            tagState.SetActive(true);
           //renderer.material.color = Color.red;
        }
        else
        {
            defaultState.SetActive(true);
            tagState.SetActive(false);
            //renderer.material.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.gameObject.GetComponentInParent<ChildrenTag>())
      {
            if (isTag == true)
            {
                StartCoroutine(activateCollider());
                minigame.updateCurrentTagged();

            }
            else
            {
                StartCoroutine(deactivateCollider());
            }

      }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------
        if (other.gameObject.GetComponent<PlayerTag>() != null) //Player
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                this.isTag = false;

                other.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
                spriteUpdate();
                StartCoroutine(other.gameObject.GetComponent<PlayerTag>().colliderCooldown());
                minigame.updateCurrentTagged();

                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                this.isTag = true;

                other.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                spriteUpdate();
                StartCoroutine(activateCollider());
                minigame.updateCurrentTagged();

                Debug.Log("Tag");
            }
        }




    }
    IEnumerator activateCollider()
    {

        yield return new WaitForSeconds(delaySpeed);
        tagCollider.SetActive(true);
    }

    IEnumerator deactivateCollider()
    {
        tagCollider.SetActive(false);
        yield return null;
    }
  
    public IEnumerator goToTarget()
    {
        while(distance >= minDistance)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, target.position, speed * Time.deltaTime);
            distance = Vector3.Distance(target.transform.position, this.transform.position);
            yield return null;
        }

        yield return new WaitForSeconds(delaySpeed);
        if (distance < minDistance)
        {
            setTarget();
            Debug.Log("NewTarget");
        }
    }

    public IEnumerator fleeTarget(Transform target)
    {
        while (distance <= fleeDistance)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, -1 * speed * Time.deltaTime);
            distance = Vector3.Distance(target.transform.position, this.transform.position);
            yield return null;
        }

    }

    public void setTarget()
    {
        if (isTag == true)
        {
            int RNG = Random.Range(0, potentialTargets.Count);

            speed = taggedSpeed;

            target = potentialTargets[RNG].transform;
            if(target == previousTag)
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

            target = randomPoints[RNG].transform;
            //target = minigame.currentTagged.transform;
            distance = Vector3.Distance(target.transform.position, this.transform.position);
            goToTargetRoutine = StartCoroutine(goToTarget());
           // StartCoroutine(fleeTarget(target));

            Debug.Log("Position Set");
        }
  
    }

}
//if (other.gameObject.GetComponentInParent<PlayerTag>().isTag == false && isTag == true)
//{
//    other.gameObject.GetComponentInParent<PlayerTag>().isTag = true;
//    this.isTag = false;
//    this.setTarget();


//    StartCoroutine(deactivateCollider());

//    spriteUpdate();
//    Debug.Log("Tag");
//}
//else if (other.gameObject.GetComponentInParent<PlayerTag>().isTag == true && isTag == false)
//{
//    other.gameObject.GetComponentInParent<PlayerTag>().isTag = false;
//    this.isTag = true;
//    this.setTarget();


//    StartCoroutine(activateCollider());

//    spriteUpdate();
//    Debug.Log("Tag");
//}