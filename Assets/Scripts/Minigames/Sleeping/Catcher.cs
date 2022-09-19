using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject   Parent; 
    public int          PlayerScore;
    public bool         CanCatch = true; 

    // Start is called before the first frame update
    void Start()
    {
        Parent = this.gameObject;
        CanCatch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanCatch) { return; }
        FallingFood collidedFood = collision.gameObject.GetComponent<FallingFood>();
        if (collidedFood)
        {
            Debug.Log("Collided with" + collidedFood.name);
            collidedFood.OnCollided(Parent);
            Destroy(collidedFood.gameObject);   
        }
    }
}
