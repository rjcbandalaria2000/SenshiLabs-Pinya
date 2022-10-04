using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickItem : MonoBehaviour
{
    private GameObject item;
    public Ingredients ingridientType;
    public GroceryManager groceryMiniGame;
    public DisplayGroceryList groceryList;
    public bool isDuplicate;
    public int quantity;

    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject;
        isDuplicate = false;
        quantity = 0;

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
       // Debug.Log("Click");
  

        if(groceryMiniGame != null)
        {
           // Debug.Log("Moving");
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
        bool isCorrect = false;

        for(int i = 0; i < groceryMiniGame.wantedItems.Count; i++)
        {
            if(ingridientType == groceryMiniGame.wantedItems[i].GetComponent<ClickItem>().ingridientType && groceryMiniGame.wantedItems[i] != null)
            {
                Debug.Log("Correct");
                StartCoroutine(lerpItem());
                groceryList.blank();
                groceryMiniGame.wantedItems.RemoveAt(i);
                groceryList.updateList();
                isCorrect = true;
                break;
            }
            //else
            //{
            //    Debug.Log("Wrong");
            //    //Vector3 shake = new Vector3(0.5f, 0, 0);
            //    //item.transform.DOShakePosition(0.3f, shake, 10, 45, false, false);
            //}
        }

        if(!isCorrect)
        {
            Debug.Log("Wrong");
            Vector3 shake = new Vector3(0.5f, 0, 0);
            item.transform.DOShakePosition(0.3f, shake, 10, 45, false, false);
        }

        //SingletonManager.Get<GroceryManager>().CheckIfFinished();
        Events.OnObjectiveUpdate.Invoke();  
        // groceryMiniGame.CheckIfFinished();
    }
}
