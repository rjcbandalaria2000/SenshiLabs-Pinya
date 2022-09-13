using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    Vector2 movement;
    public bool isTag;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    // Start is called before the first frame update
    void Start()
    {
        isTag = false;
        rb = this.GetComponent<Rigidbody2D>();
     
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

       
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

   //public void spriteUpdate()
   // {
   //     if(isTag)
   //     {
   //         this.GetComponent<SpriteRenderer>().sprite = TagSprite; 
   //         Debug.Log("Tag Sprite");
   //     }
   //     else
   //     {
   //         this.GetComponent<SpriteRenderer>().sprite = defaultSprite;
   //         Debug.Log("Default Sprite");
   //     }
   // }


   // private void OnTriggerEnter2D(Collider2D other)
   // {
   //     if (other.gameObject.GetComponent<ChildrenTag>() != null)
   //     {
   //         if (other.gameObject.GetComponent<ChildrenTag>().isTag == false && isTag == true)
   //         {
   //             other.gameObject.GetComponent<ChildrenTag>().isTag = true;
   //             isTag = false;
   //             spriteUpdate();
   //             Debug.Log("Tag");
   //         }
   //     }
   // }
}
