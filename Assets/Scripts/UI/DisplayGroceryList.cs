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
    public List<int> quantities;
    public List<bool> isDuplicated;

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
        int points = 0;

        while (index < itemsNeeded.Count)
        {

            for (int i = index + 1; i < itemsNeeded.Count; i++)
            {
                //if(index == i)
                //{
                //    Debug.Log("Same Index");
                //}
                if (grocery.wantedItems[index] == itemsNeeded[i])
                {
                    Debug.Log("Duplicate");
                    points++;
                    itemsNeeded.RemoveAt(i);
                }
              
            }


            quantities.Add(points);
            points = 0;

            index++;

        }



        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            GameObject WantedItem = Instantiate(imgUI, postionUI[i].position, Quaternion.identity);
            WantedItem.transform.SetParent(parentCanvas.transform);
            WantedItem.GetComponent<Image>().sprite = itemsNeeded[i].GetComponent<SpriteRenderer>().sprite;
            //WantedItem.GetComponent<DisplayItemCounter>().quantity += quantities[i];
            //WantedItem.GetComponent<DisplayItemCounter>().counter.text = WantedItem.GetComponent<DisplayItemCounter>().quantity + "x";
            //if(isDuplicated[i] == true)
            //{
            //    WantedItem.GetComponent<DisplayItemCounter>().quantity += quantities[i];
            //    WantedItem.GetComponent<DisplayItemCounter>().counter.text = WantedItem.GetComponent<DisplayItemCounter>().quantity + "x";
            //}



            UIitems.Add(WantedItem);
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

    //public void duplicateCheck()
    //{
    //    itemsNeeded = grocery.wantedItems;
    //    int index = 0;
       

    //    while (index < grocery.wantedItems.Count)
    //    {

    //        for (int i = index + 1; i < itemsNeeded.Count; i++)
    //        {
    //            if (index == i)
    //            {
    //                Debug.Log("Same Item");

    //            }
    //            else if (itemsNeeded[index] == grocery.wantedItems[i])
    //            {
    //                Debug.Log("Duplicate");

                

    //            }
    //        }

    //        index++;
    //    }

    //}
}
