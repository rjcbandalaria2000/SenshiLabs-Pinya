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

    [SerializeField]private GameObject basket;
    private Vector3 basketPosition;
    
    public int QuantityOfIngredient;
    public List<GameObject> spawnPoints;

    private int RNG;

    Coroutine initializeRoutine;
    Coroutine addAvailableItemsRoutine;
    Coroutine spawnRoutine;
   
    private void Awake()
    {
        SingletonManager.Register(this);

       
    }

    void Start()
    {
        numberOfItems = Random.Range(2, 5);
        sceneChange = this.gameObject.GetComponent<SceneChange>();

        if (groceryItems.Count > 0)
        {
          
            addAvailableItemsRoutine = StartCoroutine(addAvailableItems());
           // initializeRoutine = StartCoroutine(initializeList());

            //Spawn
            spawnRoutine = StartCoroutine(spawnItem());

            initializeRoutine = StartCoroutine(initializeList());

            //InitializeList

        }



        if (basket == null)
        {
            basket = GameObject.FindGameObjectWithTag("Basket"); // Might change this
            basketPosition = basket.transform.position;
        }
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
            SingletonManager.Get<DisplayGroceryList>().updateList();

            yield return null;
        }
    }


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
            Debug.Log("Minigame complete");
            SingletonManager.Get<PlayerData>().IsGroceryFinished = true;
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfNextScene);
        }

    }

    public override void OnMinigameLose()
    {
        Debug.Log("Minigame lose");
        Assert.IsNotNull(sceneChange, "Scene change is null or not set");
        sceneChange.OnChangeScene(NameOfNextScene);
    }

}
