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

    [Header("States")]
    public GameObject normalState;
    public GameObject tagState;

    [Header("Animation")]
    public Animator animator;
   

    void Start()
    {
        isTag = false;
        rb = this.GetComponent<Rigidbody2D>();
        renderer = this.GetComponent<SpriteRenderer>();

        animator = normalState.GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) { return; }

            animator.SetBool("IsIdle", false);
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
        else
        {
            animator.SetBool("IsIdle", true);
        }

    }

    public void spriteUpdate()
    {
        if (isTag)
        {
            normalState.SetActive(false);
            tagState.SetActive(true);

            animator = tagState.GetComponent<Animator>();
        }
        else
        {
            normalState.SetActive(true);
            tagState.SetActive(false);

            animator = normalState.GetComponent<Animator>();
        }
    }

}
