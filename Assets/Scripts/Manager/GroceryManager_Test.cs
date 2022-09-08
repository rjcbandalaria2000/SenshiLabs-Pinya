using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ingredients // Use for comparing
{
    egg,
    vegetables,
    fruit,
    rice,
    meat
};


public class GroceryManager_Test : MonoBehaviour //Might rename this
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
  
    // Start is called before the first frame update
    void Start()
    {
        numberOfItems = Random.Range(1, 5);

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
}
