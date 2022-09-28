using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGroceryList : MonoBehaviour
{
    //public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>(5);

    public List<Image> wantImage = new List<Image>(5);
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
            wantImage[i].gameObject.SetActive(false);
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
        Debug.Log("Update List");
        for (int i = 0; i < wantImage.Count;i++)
        {


            //textList[i].text = grocery.wantedItems[i].gameObject.name.ToString();


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
            wantImage[i].sprite = null;
            wantImage[i].gameObject.SetActive(false);
        }
           
    }

    public void duplicateCheck()
    {
        itemsNeeded = grocery.wantedItems;
        int index = 0;
        int imageIndex = 0;
       
       while(index < grocery.wantedItems.Count)
       {
            
            for (int i = index + 1; i < itemsNeeded.Count; i++)
            {

                if (itemsNeeded[index] == grocery.wantedItems[i])
                {

                    Debug.Log("Duplicate");

                    
                 
                }
                else
                {
                    //wantImage[imageIndex].gameObject.SetActive(true);
                    //wantImage[imageIndex].sprite = grocery.wantedItems[imageIndex].gameObject.GetComponent<SpriteRenderer>().sprite;
                    //imageIndex++;
                }

            }

            index++;
        }
       
    }
}
