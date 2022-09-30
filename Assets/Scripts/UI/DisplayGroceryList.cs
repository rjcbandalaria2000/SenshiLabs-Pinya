using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGroceryList : MonoBehaviour
{
    [Header("List UI")]
    public List<Transform> postionUI = new List<Transform>(5);
    public GameObject imgUI;
    public GameObject parentCanvas;

    public List<GameObject> itemsNeeded = new();
    public List<GameObject> UIitems;

    [SerializeField]private GroceryManager grocery;


    private void Awake()
    {
        SingletonManager.Register(this);

        grocery = GameObject.FindObjectOfType<GroceryManager>();

        
    }

    void Start()
    {
        parentCanvas = this.gameObject;
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
        itemsNeeded = new(grocery.wantedItems);
        int index = 0;

        while(index < grocery.wantedItems.Count)
        {
            
            for (int i = index + 1 ; i < itemsNeeded.Count; i++)
            {
                if(grocery.wantedItems[index] == itemsNeeded[i])
                {
                    Debug.Log("Duplicate");
                    
                    itemsNeeded.RemoveAt(i);
                }
            }

            
            index++;
           
        }

 

    }



    public void blank()
    {
        Debug.Log("Clear");

        for(int i = 0; i < UIitems.Count; i++)
        {
            Destroy(UIitems[i]);
        }

        UIitems.Clear();
           
    }

    public void duplicateCheck()
    {
        itemsNeeded = grocery.wantedItems;
        int index = 0;
        int imageIndex = 1;

        while (index < grocery.wantedItems.Count)
        {

            for (int i = index + 1; i < itemsNeeded.Count; i++)
            {
                if (index == i)
                {
                    Debug.Log("Same Item");

                }
                else if (itemsNeeded[index] == grocery.wantedItems[i])
                {
                    Debug.Log("Duplicate");

                

                }
            }

            index++;
        }

    }
}
