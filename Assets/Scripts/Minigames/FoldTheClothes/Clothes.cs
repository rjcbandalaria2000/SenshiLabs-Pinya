using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Clothes : MonoBehaviour
{
    public FoldingMinigameManager foldManager;

    [Header("Sprite States")]
    public List<Sprite> stateSprites;

    [Header("Number of Clothes")]
    public int clothes;

   [Header("Fold States")]
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

    [Header("Arrow")]
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject downArrow;

    Coroutine startTransitionRoutine;
    Coroutine endTransitionRoutine;

    // Start is called before the first frame update
    void Start()
    {
        if(foldManager == null)
        {
            if(GameObject.FindObjectOfType<FoldingMinigameManager>() != null)
            {
                foldManager = GameObject.FindObjectOfType<FoldingMinigameManager>().GetComponent<FoldingMinigameManager>();
            }
        }

        this.GetComponent<SpriteRenderer>().sprite = stateSprites[0];

        startPos = foldManager.startPos;
        middlePos = foldManager.middlePos;
        endPos = foldManager.endPos;

        leftArrow.SetActive(true);
        upArrow.SetActive(false);
        downArrow.SetActive(false);

        leftFold = false;
        topFold = false;
        downFold = false;

        mainCamera = Camera.main;
        clothes = 2;

       // this.transform.position = startPos.transform.position;
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
            if(leftFold == false && topFold == false && downFold == false)
            {
                leftFold = true;

                this.GetComponent<SpriteRenderer>().sprite = stateSprites[1];

                leftArrow.SetActive(false);
                upArrow.SetActive(true);
            }
            // if the mouse moved to the left
            Debug.Log("Fold right");
        }
       
        if(mousePosition.normalized.y > SwipeUpAccept)
        {
            if(leftFold == true && topFold == false && downFold == false)
            {
                topFold = true;

                this.GetComponent<SpriteRenderer>().sprite = stateSprites[2];

                upArrow.SetActive(false);
                downArrow.SetActive(true);
            }
          

            Debug.Log("Fold up");
        }
        if (mousePosition.normalized.y < SwipeDownAccept)
        {
            if (leftFold == true && topFold == true && downFold == false)
            {
                downFold = true;
            }
            Debug.Log("DownFoldw");
        }

        if (leftFold == true && topFold == true && downFold == true)
        {
            if( clothes > 0)
            {
                Reset();
                clothes--;

            }
            else
            {
                Debug.Log("Success");
                this.gameObject.SetActive(false);
                foldManager.CheckIfFinished();
                
               
            }
            
        }

    }

    IEnumerator StartTransition()
    {
        while(this.transform.position != middlePos.transform.position)
        {
          //  Debug.Log(Vector2.Distance(this.transform.position, middlePos.transform.position));
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

        yield return new WaitForSeconds(5f);
        Reset();
       

    }

    private void Reset()
    {
        // StopCoroutine(endTransitionRoutine);
        leftArrow.SetActive(true);
        upArrow.SetActive(false);
        downArrow.SetActive(false);

        this.GetComponent<SpriteRenderer>().sprite = stateSprites[0];

        leftFold = false;
        topFold = false;
        downFold = false;

        this.transform.position = startPos.transform.position;

        startTransitionRoutine = StartCoroutine(StartTransition());
    }
}
