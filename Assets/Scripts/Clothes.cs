using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clothes : MonoBehaviour
{
    [Header("Sprite States")]
    public List<Sprite> stateSprites; 

    [Header("Fold States")]
    [SerializeField] private bool rightFold;
    [SerializeField] private bool leftFold;
    [SerializeField] private bool topFold;
    [SerializeField] private bool downFold;

    private Camera mainCamera;
    private Vector2 initialPos;

    BoxCollider2D boxCollider;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float SwipeLeftAccept = -0.5f;
    [Range(0f, 1f)]
    public float SwipeRightAccept = 0.5f;
    [Range(0f, 1f)]
    public float SwipeUpAccept = 0.5f;
    [Range(0f, -1f)]
    public float SwipeDownAccept = -0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rightFold = false;
        leftFold = false;
        topFold = false;
        downFold = false;

        mainCamera = Camera.main;

    }

    private void OnMouseDown()
    {
        initialPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector3)initialPos;

        if (mousePosition.normalized.x < SwipeLeftAccept)
        {
            // if the mouse moved to the left
            leftFold = true;

            Debug.Log("Fold right");
        }
        if (mousePosition.normalized.x > SwipeRightAccept)
        {
            // if the mouse moved to the right 
            rightFold = true;

            Debug.Log("Fold left");
        }
        if(mousePosition.normalized.y > SwipeUpAccept)
        {
            topFold = true;

            Debug.Log("Fold up");
        }
        if(mousePosition.normalized.y < SwipeDownAccept)
        {
            downFold = true;

            Debug.Log("Fold down");
        }
        if(rightFold == true && leftFold == true && topFold == true && downFold == true)
        {
            Debug.Log("Success");
        }

    }

}
