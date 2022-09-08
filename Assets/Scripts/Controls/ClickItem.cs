using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    public GameObject item;
    public GroceryManager_Test groceryMiniGame;

    // Start is called before the first frame update
    void Start()
    {
        item = this.gameObject;

        if(groceryMiniGame == null)
        {
            groceryMiniGame = GameObject.FindObjectOfType<GroceryManager_Test>();
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
            StartCoroutine(lerpItem());
        }
    }

    IEnumerator lerpItem()
    {
        while (this.gameObject.transform.position != groceryMiniGame.basketPosition)
        {
            item.transform.position = Vector2.Lerp(this.item.transform.position, groceryMiniGame.basketPosition,1f * Time.deltaTime);
            yield return null;
        }

       
    }
}
