using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGroceryList : MonoBehaviour
{
    //public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>(5);

    public List<Image> wantImage = new List<Image>(5);
    public List<DisplayItemCounter> displayItemCounters = new List<DisplayItemCounter>(5);
    //public List<TextMeshProUGUI> duplicateCounter = new List<TextMeshProUGUI>(5);

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
        for(int i = 0; i < wantImage.Count; i++)
        {
            //textList[i].text = " ";
            wantImage[i].gameObject.SetActive(true);
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
        Debug.Log("Update List");
        for (int i = 0; i < grocery.wantedItems.Count; i++)
        {


            wantImage[i].gameObject.SetActive(true);
            wantImage[i].sprite = grocery.wantedItems[i].gameObject.GetComponent<SpriteRenderer>().sprite;
        }

        duplicateCheck();
    }



    public void blank()
    {
        Debug.Log("Clear");
        for (int i = 0; i < grocery.wantedItems.Count; i++)
        {
            //textList[i].text = " ";
            wantImage[i].gameObject.SetActive(false);
            wantImage[i].sprite = null;
          
        }
           
    }

    public void duplicateCheck()
    {
        itemsNeeded = grocery.wantedItems;
        int index = 0;
        //int imageIndex = 0;
       
       while(index < grocery.wantedItems.Count)
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
                    DisplayItemCounter itemCounter = wantImage[index].gameObject.GetComponent<DisplayItemCounter>();
                    itemCounter.quantity += 1;
                    itemCounter.counter.text = itemCounter.quantity + "x";


                    wantImage[i].gameObject.SetActive(false);

                    //imageIndex++;

                }


            }
          
            index++;
        }
       
    }
}
