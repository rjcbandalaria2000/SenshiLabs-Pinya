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

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    [Header("Location")]
    public Vector2 startPos;
    public Vector2 targetPos;
    public float speed;
    public float delaySpeed;

    Coroutine movementRoutine;
    // Start is called before the first frame update
    void Start()
    {
       

        startPos = this.transform.position;
        targetPos = RNG_Position();

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
    public void spriteUpdate(Collider2D other)
    {
        if (isTag)
        {
            this.GetComponent<SpriteRenderer>().sprite = TagSprite;

            if(other.gameObject.GetComponent<PlayerTag>())
            {
                other.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<PlayerTag>().defaultSprite;
            }
          

            if (other.gameObject.GetComponent<ChildrenTag>())
            {
                other.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<ChildrenTag>().defaultSprite;
            }
           
         
            Debug.Log("Tag Sprite");
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
           
            if (other.gameObject.GetComponent<PlayerTag>())
            {
                other.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<PlayerTag>().TagSprite;
            }
          
            if (other.gameObject.GetComponent<ChildrenTag>())
            {
                other.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<ChildrenTag>().TagSprite;
            }
           
            Debug.Log("Default Sprite");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ChildrenTag>() != null) //other AI
        {
      
            if (other.gameObject.GetComponent<ChildrenTag>().isTag == false && this.isTag == true)
            {
                other.gameObject.GetComponent<ChildrenTag>().isTag = true;
                this.isTag = false;
                spriteUpdate(other);
                Debug.Log("Tag");
            }
           

        }

        if(other.gameObject.GetComponent<PlayerTag>() != null) //Player
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                this.isTag = false;
                spriteUpdate(other);
                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                this.isTag = true;
                spriteUpdate(other);
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
