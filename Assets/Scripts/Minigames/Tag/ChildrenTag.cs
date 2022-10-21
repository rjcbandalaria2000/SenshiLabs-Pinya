using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenTag : MonoBehaviour
{
    public bool isTag;

    [Header("Bounds")]
    //public List<GameObject> points;
    public BoxCollider2D bound;
    private Vector2 size;
    private Vector2 center;

    [Header("Tag")]
    public GameObject tagCollider;


    [Header("Location")]
    public Vector2 startPos;
    public Vector2 targetPos;
    public float speed;
    public float delaySpeed;

    [Header("Target")]
    public Transform target;

    public SpriteRenderer renderer;

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;

    public TagMiniGameManager minigame;
   
    // Start is called before the first frame update
    void Start()
    {
       
       renderer = this.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            spriteUpdate();
        }

        if(bound == null)
        {
            bound = GameObject.FindGameObjectWithTag("Bound").GetComponent<BoxCollider2D>();
        }

        startPos = this.transform.position;
        targetPos = RNG_Position();

        if(minigame == null)
        {
            if(GameObject.FindObjectOfType<TagMiniGameManager>() != null)
            {
                minigame = GameObject.FindObjectOfType<TagMiniGameManager>().GetComponent<TagMiniGameManager>();
            }
        }

        movementRoutine = StartCoroutine(movement());
    }

    public Vector2 RNG_Position()
    {
        Transform boxBound = bound.GetComponent<Transform>();
        center = boxBound.position;
        size.x = boxBound.localScale.x * bound.size.x;
        size.y = boxBound.localScale.y * bound.size.y;


        Vector2 randomPos = new Vector2(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2));
        return center + randomPos;
    }
    public void spriteUpdate()
    {
        if (isTag)
        {
           renderer.material.color = Color.red;

            Debug.Log("Tag Sprite");
        }
        else
        {
            renderer.material.color = Color.white;


            Debug.Log("Default Sprite");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(isTag == true)
        {
            StartCoroutine(activateCollider());
        }
        else
        {
            StartCoroutine(deactivateCollider());
        }
      
       
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if(other.gameObject.GetComponent<PlayerTag>() != null) //Player
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                this.isTag = false;

                other.gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
                spriteUpdate();
                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                this.isTag = true;

                other.gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
                spriteUpdate();
                Debug.Log("Tag");
            }
        }

       
        
    }

    IEnumerator movement()
    {
        while (true)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, targetPos, speed * Time.deltaTime);
           
            
            yield return new WaitForFixedUpdate();
            
           

            if (Vector2.Distance(this.transform.position, targetPos) <= 1f)
            {
                startPos = targetPos;
                targetPos = RNG_Position();
                //Debug.Log("New Pos");
                yield return new WaitForSeconds(delaySpeed);
            }

        }
        
        
    }

    IEnumerator activateCollider()
    {
        yield return new WaitForSeconds(1.0f);
        tagCollider.SetActive(true);
    }

    IEnumerator deactivateCollider()
    {
        tagCollider.SetActive(false);
        yield return null;
    }
  
    public void goToTarget()
    {
        if(this.gameObject.transform.position != target.position && target != null)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, target.position, speed * Time.deltaTime);
        }
    }
   

}
