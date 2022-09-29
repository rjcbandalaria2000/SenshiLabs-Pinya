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

    //public List<Image> wantImage = new List<Image>(5);
    //public List<DisplayItemCounter> displayItemCounters = new List<DisplayItemCounter>(5);
    

    public List<GameObject> itemsNeeded;

    [SerializeField]private GroceryManager grocery;
    // Start is called before the first frame update

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

        for(int i = 0; i < itemsNeeded.Count; i++)
        {
            GameObject WantedItem = Instantiate(imgUI, postionUI[i].position, Quaternion.identity);
            WantedItem.transform.SetParent(parentCanvas.transform);
            WantedItem.GetComponent<Image>().sprite = itemsNeeded[i].GetComponent<SpriteRenderer>().sprite;
        }

    }



    public void blank()
    {
        Debug.Log("Clear");
        for (int i = 0; i < grocery.wantedItems.Count; i++)
        {
            //textList[i].text = " ";
            //wantImage[i].gameObject.SetActive(false);
            //wantImage[i].sprite = null;

            //DisplayItemCounter itemCounter = wantImage[i].gameObject.GetComponent<DisplayItemCounter>();
            //itemCounter.isDuplicated = false;
            //itemCounter.resetQuantity();
           

        }
           
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

                    // wantImage[index].gameObject.SetActive(true);
                    //DisplayItemCounter itemCounter = wantImage[index].gameObject.GetComponent<DisplayItemCounter>();
                    //itemCounter.quantity += 1;
                    //itemCounter.counter.text = itemCounter.quantity + "x";


                    //wantImage[imageIndex].sprite = null;

                    //imageIndex++;

                }
            }

            index++;
        }

    }
}
