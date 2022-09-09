using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public enum Ingredients // Use for comparing
{
    egg,
    vegetables,
    fruit,
    rice,
    meat
};


public class GroceryManager : MonoBehaviour //Might rename this
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

    [Header("Scene Change")]
    public string NameOfScene;

    private SceneChange sceneChange;

    // Start is called before the first frame update
    void Start()
    {
        numberOfItems = Random.Range(1, 5);
        sceneChange = this.gameObject.GetComponent<SceneChange>();

        if (groceryItems.Count > 0)
        {
            //InitializeList
            StartCoroutine(initializeList());

            //Spawn
            StartCoroutine(spawnItem());

        }

        if(basket == null)
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
            RNG = Random.Range(0, groceryItems.Count);
            GameObject item = Instantiate(groceryItems[RNG], spawnPoints[i].transform.position, Quaternion.identity);
            yield return null;
        }
    }

    IEnumerator initializeList()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            RNG = Random.Range(0, groceryItems.Count);
            needItems.Add(groceryItems[RNG]);

           
            yield return null;
        }
    }

    public void checkItemList()
    {
        if (needItems.Count <= 0)
        {
            Debug.Log("Minigame complete");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfScene);
        }
        else
        {
            Debug.Log("Minigame Fail");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfScene);
        }
    }

}
