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

    [Header("Tag")]
    public GameObject tagCollider;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    [Header("Location")]
    public Vector2 startPos;
    public Vector2 targetPos;
    public float speed;
    public float delaySpeed;

    public SpriteRenderer renderer;

    public Coroutine movementRoutine;
    public Coroutine canTagRoutine;

    private float time;
    private float delay;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        delay = 1;
       renderer = this.GetComponent<SpriteRenderer>();

        if (!isTag)
        {
            canTag = true;
        }

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
       
//------------------------------------------------------------------------------------------------------------------------------------------------------------------

        if(other.gameObject.GetComponent<PlayerTag>() != null) //Player
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                this.isTag = false;

                other.gameObject.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<PlayerTag>().TagSprite;
                spriteUpdate();
                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                this.isTag = true;

                other.gameObject.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<PlayerTag>().defaultSprite;
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

  
   
}
