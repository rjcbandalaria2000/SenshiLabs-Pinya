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

    public List<GameObject> itemsNeeded;
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
        //for(int i = 0; i < postionUI.Count; i++)
        //{
        //    //textList[i].text = " ";
        //    wantImage[i].gameObject.SetActive(true);
        //}


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
        itemsNeeded = grocery.wantedItems;
        int index = 0;

        while(index < itemsNeeded.Count)
        {
            GameObject WantedItem = Instantiate(imgUI, postionUI[index].position, Quaternion.identity);
            WantedItem.transform.SetParent(parentCanvas.transform);
            WantedItem.GetComponent<Image>().sprite = itemsNeeded[index].GetComponent<SpriteRenderer>().sprite;
            UIitems.Add(WantedItem);
            
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
