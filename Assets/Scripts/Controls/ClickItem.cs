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
    SFXManager sFX;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    public Vector3 itemPos;

    public PolygonCollider2D collider;

    // Start is called before the first frame update

    private void Awake()
    {
        sFX = GetComponent<SFXManager>();
    }
    void Start()
    {
        item = this.gameObject;
        isDuplicate = false;
        quantity = 0;
        itemPos = item.transform.position;
        if(this.GetComponent<PolygonCollider2D>() != null )
        {
            collider = this.GetComponent<PolygonCollider2D>();
            collider.enabled = true;
        }
     
        if (groceryMiniGame == null)
        {
            groceryMiniGame = GameObject.FindObjectOfType<GroceryManager>();
            groceryList = GameObject.FindObjectOfType<DisplayGroceryList>();
        }
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
                groceryMiniGame.Shake();
                groceryList.updateList();
                isCorrect = true;
                collider.enabled= false;
                sFX.PlaySFX(correctSFX);
                break;
            }
           
        }

        if(!isCorrect)
        {
            Debug.Log("Wrong");
            Vector3 shake = new Vector3(0.5f, 0, 0);

            this.item.transform.DOShakePosition(0.3f, shake, 10, 45, false, false).OnComplete(ResetPos);
            sFX.PlaySFX(wrongSFX);
        }

       
        Events.OnObjectiveUpdate.Invoke();  
        
    }

    public void ResetPos()
    {
        this.item.transform.position = itemPos;
    }
}
