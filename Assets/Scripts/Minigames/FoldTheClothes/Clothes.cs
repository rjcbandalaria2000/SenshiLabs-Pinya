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

   public bool canSwipe = true;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

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
        
        if(!canSwipe)
        {
            return;
        }

        if (mousePosition.normalized.x < SwipeLeftAccept)
        {
            canSwipe = false;
            if (leftArrow.activeSelf == true)
            {
                dragChecker();
          
                FoldRandom();
            }
            // if the mouse moved to the left
            Debug.Log("Fold left");
        }
       
        else if(mousePosition.normalized.y > SwipeUpAccept)
        {
            canSwipe = false;
            if (upArrow.activeSelf == true)
            {
                dragChecker();

                //upArrow.SetActive(false);
                //downArrow.SetActive(true);


                FoldRandom();

            }
          
            Debug.Log("Fold up");
        }
        else if (mousePosition.normalized.y < SwipeDownAccept)
        {
            canSwipe = false;
            if (downArrow.activeSelf == true)
            {

                dragChecker();


                 FoldRandom();
            }

            Debug.Log("DownFold");
        }

        if (leftFold == true && topFold == true && downFold == true)
        {
            if( clothes > 0)
            {
                clothes--;
                Reset();
             
                
            }
            else
            {
                Debug.Log("Success");
                this.gameObject.SetActive(false);
                foldManager.CheckIfFinished();
                
               
            }
            
        }

        

    }
    public void OnMouseUp()
    {
        canSwipe = true;
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
        SingletonManager.Get<DisplayFoldCount>().UpdateFoldCount();

        if(clothes == 1)
        {
            spriteChanger(4);
        }
        else
        {
            spriteChanger(0);
        }
        

        leftFold = false;
        topFold = false;
        downFold = false;
        //listArrow.Clear();

        this.transform.position = startPos.transform.position;
        foldResetRoutine = StartCoroutine(addFoldList());

        RNG = Random.Range(0, listArrow.Count);

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

   public void FoldRandom()
    {
        if(listArrow.Count > 0)
        {
            RNG = Random.Range(0, listArrow.Count);
            listArrow[RNG].SetActive(true);
            Debug.Log("FoldPIck");
        }

    }

    public void dragChecker()
    {
        for(int i =0; i < listArrow.Count; i++)
        {
            if (listArrow[i] == leftArrow && leftArrow.activeSelf == true)
            {
                leftFold = true;
                leftArrow.SetActive(false);

                if(clothes == 2)
                {
                    spriteChanger(1);
                }
                else if(clothes == 1 )
                {
                    spriteChanger(5);
                }
                else
                {
                    spriteChanger(1);
                }
               

                //leftArrow.SetActive(false);
                //upArrow.SetActive(true);
                listArrow.RemoveAt(i);
                Debug.Log(listArrow.Count);
                break;

            }
            else if(listArrow[i] == upArrow && upArrow.activeSelf == true)
            {
                topFold = true;
               upArrow.SetActive(false);

                if(clothes >= 2)
                {
                    spriteChanger(2);
                }
                else if (clothes == 1)
                {
                    spriteChanger(6);
                }
                else
                {
                    spriteChanger(2);
                }

                //leftArrow.SetActive(false);
                //upArrow.SetActive(true);
                listArrow.RemoveAt(i);
                Debug.Log(listArrow.Count);
                break;

            }
            else if (listArrow[i] == downArrow && downArrow.activeSelf == true)
            {
                downFold = true;
                downArrow.SetActive(false);

                if(clothes >= 2)
                {
                    spriteChanger(3);
                }
                else if (clothes == 1)
                {
                    spriteChanger(7);
                }
                else
                {
                    spriteChanger(3);
                }


                //leftArrow.SetActive(false);
                //upArrow.SetActive(true);
                listArrow.RemoveAt(i);
                Debug.Log(listArrow.Count);
                break;

            }
        }
        
    }

    IEnumerator addFoldList()
    {
        listArrow.Add(leftArrow);
  
        listArrow.Add(upArrow);

        listArrow.Add(downArrow);

        yield return null;

        //foldResetRoutine = null;
    }

    public void  spriteChanger(int index)
    {
        this.GetComponent<SpriteRenderer>().sprite = stateSprites[index];
    }
}
