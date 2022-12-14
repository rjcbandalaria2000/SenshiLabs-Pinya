using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Reference: https://www.youtube.com/watch?v=Ehk9fKBwS3Y
public class MouseFollow : MonoBehaviour
{
    public float        MoveSpeed = 10f;
    public bool         canMove = true;

    private Vector3     mousePosition;
    private Rigidbody2D rigidBody;
    private Vector2     position = new();
    private Camera      mainCamera;




    void Start()
    {
        if(rigidBody == null)
        {
            rigidBody = this.GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;
        canMove = true;
    }

    private void Update()
    {
        if (canMove)
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            position = Vector2.Lerp(transform.position, mousePosition, MoveSpeed * Time.deltaTime);
        }
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rigidBody.MovePosition(position);
        }
       
    }

   

}
