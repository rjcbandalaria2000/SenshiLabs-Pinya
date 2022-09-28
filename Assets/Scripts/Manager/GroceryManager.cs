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
    private float GameStartTimer = 0;

    private Coroutine startMinigameRoutine;
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
            SingletonManager.Get<MiniGameTimer>().decreaseValue = 0;
            objectList.SetActive(false);
        }

    }

    public override void OnMinigameLose()
    {

        SingletonManager.Get<UIManager>().ActivateResultScreen();

 
    }

    public override void Initialize()
    {
        SingletonManager.Get<UIManager>().ActivateMiniGameMainMenu();
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        objectList.SetActive(false); 
        startMinigameRoutine = null;

       
    }

    public override void StartMinigame()
    {
        GameStartTimer = GameStartTime;
        SingletonManager.Get<UIManager>().ActivateGameCountdown();
        startMinigameRoutine = StartCoroutine(StartMinigameCounter());

        objectList.SetActive(true);

        SingletonManager.Get<UIManager>().DeactivateMiniGameMainMenu();
        SingletonManager.Get<UIManager>().ActivateMiniGameTimerUI();

        numberOfItems = 5;
        sceneChange = this.gameObject.GetComponent<SceneChange>();
  

        setUpGroceryRoutine = StartCoroutine(initialGrocery());

       
        if (basket == null)
        {
            basket = GameObject.FindGameObjectWithTag("Basket"); // Might change this
            basketPosition = basket.transform.position;
        }

    }

    public IEnumerator StartMinigameCounter()
    {
        CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
        while (GameStartTimer > 0)
        {
            GameStartTimer -= 1 * Time.deltaTime;
            CountdownTimerUI.UpdateCountdownTimer(GameStartTimer);
            yield return null;
        }
        //yield return new WaitForSeconds(GameStartTime);
        SingletonManager.Get<UIManager>().DeactivateGameCountdown();
        SingletonManager.Get<MiniGameTimer>().StartCountdownTimer();

        isCompleted = false;
        Events.OnObjectiveUpdate.Invoke();
        
    }

    public IEnumerator initialGrocery()
    {

        yield return new WaitForSeconds(GameStartTimer);

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
        SingletonManager.Get<PlayerData>().IsGroceryFinished = true;
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

    public void gameOver()
    {
        Debug.Log("Minigame lose");
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }
}
