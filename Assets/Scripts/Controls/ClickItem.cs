using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    private GameObject item;
    public Ingredients ingridientType;
    public GroceryManager groceryMiniGame;
    public DisplayGroceryList groceryList;
    public int Quantity;

    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject;
        Quantity = 1;

        if (groceryMiniGame == null)
        {
            groceryMiniGame = GameObject.FindObjectOfType<GroceryManager>();
            groceryList = GameObject.FindObjectOfType<DisplayGroceryList>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
  

        if(groceryMiniGame != null)
        {
            Debug.Log("Moving");
            checkItem();
        }
    }

    IEnumerator lerpItem()
    {
        while (this.gameObject.transform.position != groceryMiniGame.getBasketPosition())
        {
            item.transform.position = Vector2.Lerp(this.item.transform.position, groceryMiniGame.getBasketPosition(),5f * Time.deltaTime);
            yield return null;
        }
    }

    public void checkItem()
    {
        for(int i = 0; i < groceryMiniGame.wantedItems.Count; i++)
        {
            if(ingridientType == groceryMiniGame.wantedItems[i].GetComponent<ClickItem>().ingridientType && groceryMiniGame.wantedItems[i] != null)
            {
                Debug.Log("Correct");
                StartCoroutine(lerpItem());
                groceryList.blank();
                groceryMiniGame.wantedItems.RemoveAt(i);
                groceryList.updateList();

                break;
            }
            else
            {
                Debug.Log("Wrong");
            }
        }

        SingletonManager.Get<GroceryManager>().CheckIfFinished();
       // groceryMiniGame.CheckIfFinished();
    }
}
