using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clothes : MonoBehaviour
{
    [Header("Sprite States")]
    public List<Sprite> stateSprites;

    [Header("Number of Clothes")]
    public int clothes;

    [Header("Fold States")]
    [SerializeField] private bool rightFold;
    [SerializeField] private bool leftFold;
    [SerializeField] private bool topFold;
    [SerializeField] private bool downFold;

    [Header("Position")]
    public GameObject startPos;
    public GameObject middlePos;
    public GameObject endPos;
    public float lerpSpeed;

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


    Coroutine startTransitionRoutine;
    Coroutine endTransitionRoutine;

    // Start is called before the first frame update
    void Start()
    {
        rightFold = false;
        leftFold = false;
        topFold = false;
        downFold = false;

        mainCamera = Camera.main;
        clothes = 2;

        this.transform.position = startPos.transform.position;
        startTransitionRoutine = StartCoroutine(StartTransition());
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
            if( clothes > 0)
            {
                Reset();
                clothes--;

            }
            else
            {
                this.gameObject.SetActive(false);
                Debug.Log("Success");
            }
            
        }

    }

    IEnumerator StartTransition()
    {
        while(this.transform.position != middlePos.transform.position)
        {
            Debug.Log(Vector2.Distance(this.transform.position, middlePos.transform.position));
            if (Vector2.Distance(this.transform.position, middlePos.transform.position) >= 0.1f )
            {
                this.transform.position = Vector2.Lerp(this.transform.position, middlePos.transform.position, lerpSpeed * Time.deltaTime);
                
            }

            yield return null;
        }
       
    }

    IEnumerator EndTransition()
    {
        while (this.transform.position != endPos.transform.position)
        {
            Debug.Log(Vector2.Distance(this.transform.position, endPos.transform.position));

            this.transform.position = Vector2.Lerp(this.transform.position, endPos.transform.position, lerpSpeed * Time.deltaTime);
         
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        Reset();
       

    }

    private void Reset()
    {
       // StopCoroutine(endTransitionRoutine);

        rightFold = false;
        leftFold = false;
        topFold = false;
        downFold = false;

        this.transform.position = startPos.transform.position;

        startTransitionRoutine = StartCoroutine(StartTransition());
    }
}
