using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenTag : MonoBehaviour
{
    public bool isTag;
    public bool canTag;

    [Header("Bounds")]
    //public List<GameObject> points;
    public BoxCollider2D bound;
    private Vector2 size;
    private Vector2 center;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    [Header("Location")]
    public Vector2 startPos;
    public Vector2 targetPos;
    public float speed;
    public float delaySpeed;

    public SpriteRenderer renderer;

    Coroutine movementRoutine;
    Coroutine canTagRoutine;
    // Start is called before the first frame update
    void Start()
    {
       renderer = this.GetComponent<SpriteRenderer>();
        canTag = true;

        if (renderer != null)
        {
            spriteUpdate();
        }

        startPos = this.transform.position;
        targetPos = RNG_Position();

        movementRoutine = StartCoroutine(movement());
    }

    private void Update()
    {
        
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
           renderer.sprite = TagSprite;

           
         
            Debug.Log("Tag Sprite");
        }
        else
        {
            renderer.sprite = defaultSprite;
           
           
            Debug.Log("Default Sprite");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChildrenTag collidedChildren = other.GetComponent<ChildrenTag>();
        PlayerTag playerTag = other.GetComponent<PlayerTag>();

        if (collidedChildren != null) //other AI
        {
            if (this.isTag == true)
            {
                if(collidedChildren.isTag == false)
                {
                    if(canTag)
                    {
                        collidedChildren.isTag = true;
                        this.isTag = false;
                        collidedChildren.spriteUpdate();
                        spriteUpdate();

                        canTagRoutine = StartCoroutine(cantTag());

                        Debug.Log("AI Tag");
                    }
                    
                }
                
            }
           

        }

        if(other.gameObject.GetComponent<PlayerTag>() != null) //Player
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                this.isTag = false;
                
                spriteUpdate();
                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                this.isTag = true;
               
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
            
            if(Vector2.Distance(this.transform.position, targetPos) <= 1f)
            {
                startPos = targetPos;
                targetPos = RNG_Position();
                //Debug.Log("New Pos");
                yield return new WaitForSeconds(delaySpeed);
            }

        }
       
    }

    IEnumerator cantTag()
    {
        canTag = false;

        yield return new WaitForSeconds(1.0f);

        canTag = true;
    }
   
}
