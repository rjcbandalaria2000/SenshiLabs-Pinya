using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Vector2 position;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //position = new Vector2(this.transform.position.x, this.transform.position.y - moveSpeed *Time.deltaTime);
       
    }

    private void FixedUpdate()
    {
         this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - moveSpeed * Time.deltaTime);

    }
}
