using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum groceryItems // Use for comparing
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
    [SerializeField]private GameObject basket;
    public Vector3 basketPosition;
    public int numberOfItems;
    public int QuantityOfIngredient;
    public List<GameObject> spawnPoints;
  
    // Start is called before the first frame update
    void Start()
    {
        if(groceryItems.Count > 0)
        {
            //Spawn
            for(int i = 0; i < spawnPoints.Count; i++)
            {
              GameObject item =  Instantiate(groceryItems[i], spawnPoints[i].transform.position,Quaternion.identity);
            }

        }

        numberOfItems = Random.Range(1, 5);
        if(basket == null)
        {
            basket = GameObject.FindGameObjectWithTag("Basket"); // Might change this
            basketPosition = basket.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
