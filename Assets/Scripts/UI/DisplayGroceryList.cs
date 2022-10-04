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
        int points = 1;

        while (index < itemsNeeded.Count)
        {

            for (int i = index + 1; i < itemsNeeded.Count; i++)
            {
               
                if (grocery.wantedItems[index] == itemsNeeded[i])
                {
                    Debug.Log("Duplicate");
                    points++;
                    itemsNeeded.RemoveAt(i);
                }
                //else
                //{
                //    points = 0;
                //}
              
            }


            quantities.Add(points);
            points = 1;
            Debug.Log("ItemList: " + itemsNeeded.Count);
    
            index++;

        }

      


        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            GameObject WantedItem = Instantiate(imgUI, postionUI[i].position, Quaternion.identity);
            WantedItem.transform.SetParent(parentCanvas.transform);
            WantedItem.GetComponent<Image>().sprite = itemsNeeded[i].GetComponent<SpriteRenderer>().sprite;
            WantedItem.GetComponent<DisplayItemCounter>().quantity = quantities[i];

            UIitems.Add(WantedItem);

            //if (UIitems[i].gameObject.GetComponent<DisplayItemCounter>() != null)
            //{
            //    UIitems[i].gameObject.GetComponent<DisplayItemCounter>().quantity = 2;
            //    UIitems[i].gameObject.GetComponent<DisplayItemCounter>().counter.text = UIitems[i].gameObject.GetComponent<DisplayItemCounter>().quantity + "x";
            //}
            //else
            //{
            //    Debug.Log("DisplayNUll");
            //}

        }
        


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
        quantities.Clear();
        UIitems.Clear();
           
    }

  
}
