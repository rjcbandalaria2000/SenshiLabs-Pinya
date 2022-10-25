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

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AITagMinigame>() != null) //other AI
        {
            if (isTag == true && other.GetComponent<AITagMinigame>().isTag == false)
            {
                other.GetComponent<AITagMinigame>().previousTag = this.gameObject;
                other.GetComponent<AITagMinigame>().updateAITag(other.GetComponent<AITagMinigame>());
                Debug.Log("AI Tag");

            }
        }
       
    }

}
