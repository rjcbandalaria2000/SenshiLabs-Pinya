using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMovement : MonoBehaviour
{
    [Range(1f, 5f)]
    public float moveSpeed;

    private Vector2 position;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
         this.transform.position = new Vector2(this.transform.position.x, 
             this.transform.position.y - moveSpeed * Time.deltaTime);
        this.transform.Rotate(0, 0, 1f);
    }
}
