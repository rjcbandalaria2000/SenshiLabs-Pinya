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

   
}
