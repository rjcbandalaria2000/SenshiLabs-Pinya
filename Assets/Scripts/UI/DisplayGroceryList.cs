using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGroceryList : MonoBehaviour
{
    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>(5);

    public List<Image> wantImage = new List<Image>(5);
    //public List<TextMeshProUGUI> duplicateCounter = new List<TextMeshProUGUI>(5);

    [SerializeField]private GroceryManager grocery;
    // Start is called before the first frame update

    private void Awake()
    {
        SingletonManager.Register(this);

        grocery = GameObject.FindObjectOfType<GroceryManager>();
    }

    void Start()
    {
        for(int i = 0; i < textList.Count; i++)
        {
            textList[i].text = " ";
            wantImage[i].gameObject.SetActive(false);
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
       
        for(int i = 0; i < grocery.wantedItems.Count;i++)
        {
            Debug.Log("Update List");
            Debug.Log(grocery.wantedItems[i].name);
            textList[i].text = grocery.wantedItems[i].gameObject.name.ToString();


            wantImage[i].gameObject.SetActive(true);
            wantImage[i].sprite = grocery.wantedItems[i].gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }



    public void blank()
    {
        Debug.Log("Clear");
        for (int i = 0; i < grocery.wantedItems.Count; i++)
        {
            textList[i].text = " ";
            wantImage[i].sprite = null;
            wantImage[i].gameObject.SetActive(false);
        }
           
    }

    public void duplicateCheck(int index, string name)
    {
        if (grocery.wantedItems[index].gameObject.name == name) ;
    }
}
