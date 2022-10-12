using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public enum Ingredients // Use for comparing
{
    egg,
    tomato,
    Kalamansi,
    sili,
    okra,
    talong
};


public class GroceryManager : MinigameManager //Might rename this
{
    [Header("Grocery Item")]
    public List<GameObject> groceryItems = new List<GameObject>();

    [Header("ItemCounter")]
    public int numberOfItems;
    public List<GameObject> itemsAvailable;
    public List<GameObject> wantedItems;
    //public List<GameObject> extraList;

    [SerializeField]private GameObject basket;
    private Vector3 basketPosition;
    
    public int QuantityOfIngredient;
    public List<GameObject> spawnPoints;
    public GameObject objectList;
    public GameObject groceryList;

    private int RNG;
   
    Coroutine initializeRoutine;
    Coroutine addAvailableItemsRoutine;
    Coroutine spawnRoutine;

    [Header("Countdown Timer")]
    public float GameStartTime = 3f;
    public DisplayGameCountdown CountdownTimerUI;
    
    private Coroutine setUpGroceryRoutine;

    private void Awake()
    {
        SingletonManager.Register(this);

       
    }

    void Start()
    {

        Initialize();

       
    }
    public Vector3 getBasketPosition()
    {
        return basketPosition;
    }
    
   

    IEnumerator wantedSpawn()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            RNG = Random.Range(0, itemsAvailable.Count);
            GameObject item = Instantiate(itemsAvailable[RNG], spawnPoints[i].transform.position, Quaternion.identity);

            yield return null;
        }
    }

    IEnumerator spawnItem()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
          
            GameObject item = Instantiate(itemsAvailable[i], spawnPoints[i].transform.position, Quaternion.identity);
            yield return null;
        }
    }

    IEnumerator initializeList()
    {
        for (int i = 0; i < numberOfItems; i++) 
        {
            RNG = Random.Range(0, itemsAvailable.Count);

            
            wantedItems.Add(itemsAvailable[RNG]);
           

            //extraList = itemsAvailable;

            yield return null;
        }
        SingletonManager.Get<DisplayGroceryList>().updateList();
    }


    //public void checkDuplicate(int num)
    //{
    //    for(int i = 0; i < wantedItems.Count; i++)
    //    {
            
    //    }
    //}

    IEnumerator addAvailableItems()
    {
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            RNG = Random.Range(0,groceryItems.Count);
            itemsAvailable.Add(groceryItems[RNG]);
        }

        yield return null;
    }


    public override void CheckIfFinished()
    {
        if (wantedItems.Count <= 0)
        {
            Debug.Log("Minigame success");
            SingletonManager.Get<UIManager>().ActivateResultScreen();
            SingletonManager.Get<UIManager>().ActivateGoodResult();
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
            objectList.SetActive(false);
        }

    }

    public override void OnMinigameLose()
    {

        SingletonManager.Get<UIManager>().ActivateResultScreen();
        SingletonManager.Get<UIManager>().ActivateBadResult();


    }

    public override void Initialize()
    {
        transitionManager = SingletonManager.Get<TransitionManager>();

        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        Events.OnSceneChange.AddListener(OnSceneChange);
        objectList.SetActive(false); 
        startMinigameRoutine = null;

       
    }

    private void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(CheckIfFinished);
        Events.OnSceneChange.RemoveListener(OnSceneChange);

    }

    public override void StartMinigame()
    {
       
        objectList.SetActive(false);
        groceryList.SetActive(false);

      

        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();

        numberOfItems = 5;
        sceneChange = this.gameObject.GetComponent<SceneChange>();


        startMinigameRoutine = StartCoroutine(StartMinigameCounter());


        

        if (basket == null)
        {
            basket = GameObject.FindGameObjectWithTag("Basket"); // Might change this
            basketPosition = basket.transform.position;
        }

    }

    protected override IEnumerator StartMinigameCounter()
    {
        gameStartTimer = GameStartTime;

       

        //Deactivate Minigame Main Menu
        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        //Start Curtain Transition
        SingletonManager.Get<TransitionManager>().ChangeAnimation(TransitionManager.CURTAIN_OPEN);
       
        //Wait for the animation to finish 
        if (transitionManager != null)
        {
            while (!transitionManager.IsAnimationFinished())
            {
                yield return null;
            }
        }
        //Activate Game Countdown
        SingletonManager.Get<UIManager>().ActivateGameCountdown();
        countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
        //countdownTimerUI.UpdateCountdownTimer(gameStartTimer);
        //Wait till the game countdown is finish
        while (gameStartTimer > 0)
        {
            gameStartTimer -= 1 * Time.deltaTime;
            countdownTimerUI.UpdateCountdownSprites((int)gameStartTimer);
            yield return null;
        }

        //After Game Countdown
        //Activate GameUI and Timer
        setUpGroceryRoutine = StartCoroutine(initialGrocery());

        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();
        
       
       
        //Events.OnObjectiveUpdate.Invoke();
        Debug.Log("Refresh Score board");
        //Spawn objects
      
        isCompleted = false;
    }

    public IEnumerator initialGrocery()
    {

        yield return null;

        objectList.SetActive(true);
        groceryList.SetActive(true);

        if (groceryItems.Count > 0)
        {

            addAvailableItemsRoutine = StartCoroutine(addAvailableItems());
            // initializeRoutine = StartCoroutine(initializeList());

            //Spawn
            spawnRoutine = StartCoroutine(spawnItem());

            initializeRoutine = StartCoroutine(initializeList());

            //InitializeList

        }
    }


    public void continueScene()
    {
        Debug.Log("Minigame complete");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    public void gameOver()
    {
        Debug.Log("Minigame lose");
        exitMinigameRoutine = StartCoroutine(ExitMinigame());
    }

    protected override IEnumerator ExitMinigame()
    {
        // Play close animation
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);
        //Deactivate active UI 
        SingletonManager.Get<UIManager>().DeactivateResultScreen();
        SingletonManager.Get<UIManager>().DeactivateTimerUI();
        SingletonManager.Get<UIManager>().DeactivateGameUI();
        //Wait for transition to end
        while (!transitionManager.IsAnimationFinished())
        {
            Debug.Log("Transition to closing");
            yield return null;
        }
        Events.OnSceneChange.Invoke();
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
        yield return null;
    }
}
