using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayGroceryList : MonoBehaviour
{
    public List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>(5);

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
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateList()
    {
       
        for(int i = 0; i < grocery.needItems.Count;i++)
        {
            Debug.Log(grocery.needItems[i].name);
            textList[i].text = grocery.needItems[i].gameObject.name.ToString();
        }
        
        
    }

    public void blank()
    {
        Debug.Log("Clear");
        for (int i = 0; i < grocery.needItems.Count; i++)
        {
            textList[i].text = " ";
        }
           
    }
}
