using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



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
    public int RNG;

    [Header("Position")]
    public GameObject startPos;
    public GameObject middlePos;
    public GameObject endPos;
    [SerializeField] private float lerpSpeed;

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

    public List<GameObject> listArrow;

    Coroutine startTransitionRoutine;
    Coroutine endTransitionRoutine;
    Coroutine foldResetRoutine;

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

        //leftArrow.SetActive(true);
        //upArrow.SetActive(false);
        //downArrow.SetActive(false);

        foldResetRoutine = StartCoroutine(addFoldList());

        leftFold = false;
        topFold = false;
        downFold = false;

        mainCamera = Camera.main;
        clothes = 2;

        // this.transform.position = startPos.transform.position;

        RNG = Random.Range(0, listArrow.Count);
        Debug.Log(listArrow.Count);
        switch (RNG)
        {
            case 0:
                listArrow[0].SetActive(true);
                break;
            case 1:
                listArrow[1].SetActive(true);
                break;
            case 2:
                listArrow[2].SetActive(true);
                break;
            default:
                Debug.Log("Nothing");
                break;
        }


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
            if(listArrow[0].activeSelf == true)
            {
                leftFold = true;

                this.GetComponent<SpriteRenderer>().sprite = stateSprites[1];

                //leftArrow.SetActive(false);
                //upArrow.SetActive(true);
                listArrow.RemoveAt(0);
            }
            // if the mouse moved to the left
            Debug.Log("Fold right");
        }
       
        if(mousePosition.normalized.y > SwipeUpAccept)
        {
            if(listArrow[1].activeSelf == true)
            {
                topFold = true;

                this.GetComponent<SpriteRenderer>().sprite = stateSprites[2];

                //upArrow.SetActive(false);
                //downArrow.SetActive(true);
                listArrow.RemoveAt(1);

            }
          

            Debug.Log("Fold up");
        }
        if (mousePosition.normalized.y < SwipeDownAccept)
        {
            if (listArrow[2].activeSelf == true)
            {
                downFold = true;
                listArrow.RemoveAt(2);
            }
            Debug.Log("DownFold");
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
        //leftArrow.SetActive(true);
        //upArrow.SetActive(false);
        //downArrow.SetActive(false);

        this.GetComponent<SpriteRenderer>().sprite = stateSprites[0];

        leftFold = false;
        topFold = false;
        downFold = false;
        //listArrow.Clear();

        this.transform.position = startPos.transform.position;
        foldResetRoutine = StartCoroutine(addFoldList());
        startTransitionRoutine = StartCoroutine(StartTransition());
    }

   public void FoldRandom()
    {
        RNG = Random.Range(0, listArrow.Count);
        listArrow[RNG].SetActive(true);
        Debug.Log("FoldPIck");
    }

    IEnumerator addFoldList()
    {
        listArrow.Add(leftArrow);
  
        listArrow.Add(upArrow);

        listArrow.Add(downArrow);

        yield return null;

        //foldResetRoutine = null;
    }
}
