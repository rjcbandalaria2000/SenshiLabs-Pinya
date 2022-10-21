using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerTag : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    Vector2 movement;
    public bool isTag;

    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite TagSprite;

    private Vector3 targetPosition;

    void Start()
    {
        isTag = false;
        rb = this.GetComponent<Rigidbody2D>();
     
    }

   
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
            
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = this.transform.position.z;
        }


        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

       
    }
    private void FixedUpdate()
    {
        //Avoids jittering 
        if (Vector3.Distance(this.transform.position, targetPosition) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        }

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

   
}
