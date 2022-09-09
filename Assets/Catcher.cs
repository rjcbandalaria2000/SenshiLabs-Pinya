using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public int PlayerScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with object");
        FallingFood collidedFood = collision.gameObject.GetComponent<FallingFood>();
        if (collidedFood)
        {
            Debug.Log("Collided with" + collidedFood.name);
            SingletonManager.Get<SleepingMinigameManager>().PlayerPoints++;
            SingletonManager.Get<SleepingMinigameManager>().CheckIfFinished();
            Destroy(collidedFood.gameObject);   
        }
    }
}
