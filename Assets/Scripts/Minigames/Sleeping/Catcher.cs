using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject   Parent; 
    public int          PlayerScore;
    public bool         CanCatch = true;
    SFXManager sFXManager;
    public AudioClip catchFood;
    public AudioClip pinyaCatch;
    // Start is called before the first frame update
    void Start()
    {
        sFXManager = GetComponent<SFXManager>();
        Parent = this.gameObject;
        CanCatch = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanCatch) { return; }
        FallingFood collidedFood = collision.gameObject.GetComponent<FallingFood>();
        if (collidedFood)
        {
            if (collidedFood.Name != "Pinya")
            {
                sFXManager.PlaySFX(catchFood);
            }
            else
            {
                sFXManager.PlaySFX(pinyaCatch);
            }
            Debug.Log("Collided with" + collidedFood.name);
            collidedFood.OnCollided(Parent);
            Destroy(collidedFood.gameObject);   
        }
    }
}
