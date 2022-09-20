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
    public List<GameObject> needItems;

    [SerializeField]private GameObject basket;
    private Vector3 basketPosition;
    
    public int QuantityOfIngredient;
    public List<GameObject> spawnPoints;

    private int RNG;

    Coroutine initializeRoutine;
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
            //InitializeList
            initializeRoutine = StartCoroutine(initializeList());

            //Spawn
            spawnRoutine = StartCoroutine(spawnItem());

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

    IEnumerator spawnItem()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            
            RNG = Random.Range(0, needItems.Count);
            GameObject item = Instantiate(needItems[RNG], spawnPoints[i].transform.position, Quaternion.identity);
            yield return null;
        }
    }

    IEnumerator initializeList()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            RNG = Random.Range(0, groceryItems.Count);
            needItems.Add(groceryItems[RNG]);
            SingletonManager.Get<DisplayGroceryList>().updateList();

            yield return null;
        }
    }

    public override void CheckIfFinished()
    {
        if (needItems.Count <= 0)
        {
            Debug.Log("Minigame complete");
            SingletonManager.Get<Player_Data>().achieveGroceryTask = true;
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
