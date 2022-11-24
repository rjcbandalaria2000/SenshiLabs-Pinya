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
    public List<int> quantities = new List<int>(5);

 
 

    public List<GameObject> itemsNeeded = new();
    public List<GameObject> UIitems;

    [SerializeField]private GroceryManager grocery;


    int points;
    int posIndex;

    private void Awake()
    {
        SingletonManager.Register(this);

        grocery = GameObject.FindObjectOfType<GroceryManager>();

        
    }

    void Start()
    {
        parentCanvas = this.gameObject;

        points = 1;
        posIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
        
        itemsNeeded = new(grocery.wantedItems);

        int index = 0;

        while (index < itemsNeeded.Count)
        {


            for (int i = index+1; i < itemsNeeded.Count;i++)
            {
               if(itemsNeeded[i] == null)
                {
                    //skip
                    Debug.Log("Skip");
                   
                }

                else if (grocery.wantedItems[index].name == itemsNeeded[i].name && itemsNeeded[i] != null)
                {
                    Debug.Log("Duplicate");
                    points++;
                    itemsNeeded[i] = null;
                    

                }
           
            }

           

            quantities.Add(points);
            points = 1;
            Debug.Log("ItemList: " + itemsNeeded.Count);

           
            index++;

        }
        //for (int i = 0; i < itemsNeeded.Count; i++)
        //{
        //    if (itemsNeeded[i] == null)
        //    {
        //        itemsNeeded.RemoveAt(i);
        //    }
        //}




        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            if(itemsNeeded[i] != null)
            {
                GameObject WantedItem = Instantiate(imgUI, postionUI[posIndex].position, Quaternion.identity);
                posIndex++;
                WantedItem.transform.SetParent(parentCanvas.transform);
                WantedItem.GetComponent<Image>().sprite = itemsNeeded[i].GetComponent<SpriteRenderer>().sprite;
                WantedItem.GetComponent<DisplayItemCounter>().quantity = quantities[i];

                UIitems.Add(WantedItem);
            }
            

        }
        posIndex = 0;


    }

    
    public void blank()
    {
        Debug.Log("Clear");
       
        for(int i = 0; i < UIitems.Count; i++)
        {
            if(UIitems[i].GetComponent<DisplayItemCounter>().quantity > 0)
            {
                Destroy(UIitems[i]);
            }
           
          
           
        }
        itemsNeeded.Clear();
        quantities.Clear();
        UIitems.Clear();
           
    }

  
}
