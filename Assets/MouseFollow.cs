using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float        MoveSpeed = 10f;
    

    private Vector3     mousePosition;
    private Rigidbody2D rigidBody;
    private Vector2     position = new();
    private Camera      mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        if(rigidBody == null)
        {
            rigidBody = this.GetComponent<Rigidbody2D>();
        }
        mainCamera = Camera.main;
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, MoveSpeed * Time.deltaTime); 
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(position);
    }

}
