using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TagMinigamePlayer : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    Vector2 movement;
    public bool isTag;
    private Vector3 targetPosition;

    public GameObject tagCollider;

    public SpriteRenderer renderer;
    void Start()
    {
        isTag = false;
        rb = this.GetComponent<Rigidbody2D>();
        renderer = this.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = this.transform.position.z;
        }
    }
    private void FixedUpdate()
    {
        //Avoids jittering 
        if (Vector3.Distance(this.transform.position, targetPosition) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        }

    }

    public void spriteUpdate()
    {
        if (isTag)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.white;
        }
    }

}
