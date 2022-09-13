using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenTag : MonoBehaviour
{
    public bool isTag;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            else
            {
                other.GetComponent<SpriteRenderer>().sprite = defaultSprite;

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
            else
            {
                other.GetComponent<SpriteRenderer>().sprite = TagSprite;

            }
            Debug.Log("Default Sprite");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ChildrenTag>() != null)
        {
            if (other.gameObject.GetComponent<ChildrenTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<ChildrenTag>().isTag = true;
                isTag = false;
                spriteUpdate(other);
                Debug.Log("Tag");
            }
            
        }
        if(other.gameObject.GetComponent<PlayerTag>() != null)
        {
            if (other.gameObject.GetComponent<PlayerTag>().isTag == false && isTag == true)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = true;
                isTag = false;
                spriteUpdate(other);
                Debug.Log("Tag");
            }
            else if (other.gameObject.GetComponent<PlayerTag>().isTag == true && isTag == false)
            {
                other.gameObject.GetComponent<PlayerTag>().isTag = false;
                isTag = true;
                spriteUpdate(other);
                Debug.Log("Tag");
            }
        }
    }
}
