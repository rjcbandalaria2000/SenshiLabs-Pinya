using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    private GameObject item;
    public Ingredients ingridientType;
    public GroceryManager groceryMiniGame;

    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject;

        if(groceryMiniGame == null)
        {
            groceryMiniGame = GameObject.FindObjectOfType<GroceryManager>();
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
            // this.gameObject.transform.position = groceryMiniGame.basketPosition;
            //// item.transform.position = Vector3.Lerp(this.item.transform.position, groceryMiniGame.basketPosition, 1.0f*Time.deltaTime);
            // // SingletonManager.Get<GroceryManager_Test>().basket.GetComponent<GameObject>().transform.position;
            // StartCoroutine(lerpItem());
            checkItem();
        }
    }

    IEnumerator lerpItem()
    {
        while (this.gameObject.transform.position != groceryMiniGame.getBasketPosition())
        {
            item.transform.position = Vector2.Lerp(this.item.transform.position, groceryMiniGame.getBasketPosition(),1f * Time.deltaTime);
            yield return null;
        }
    }

    public void checkItem()
    {
        for(int i = 0; i < groceryMiniGame.needItems.Count; i++)
        {
            if(ingridientType == groceryMiniGame.needItems[i].GetComponent<ClickItem>().ingridientType && groceryMiniGame.needItems[i] != null)
            {
                StartCoroutine(lerpItem());
                groceryMiniGame.needItems.RemoveAt(i);
                break;
            }
            else
            {
                Debug.Log("Wrong");
            }
        }
    }
}
