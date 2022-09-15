using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Interact(GameObject gameObject = null)
    {

    }

    public void FinishInteract(GameObject gameObject = null)
    {

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Toy collidedToy = collision.gameObject.GetComponent<Toy>();
        if (collidedToy)
        {
            Debug.Log("Collided with toy");
            //if the toy has been picked up and is not held anymore, accept it in the toybin
            if (collidedToy.IsPickedUp && !collidedToy.IsHolding)
            {
                Debug.Log("Accept toy");
                SingletonManager.Get<CleanTheHouseManager>().AddTrashThrown(1);
                Destroy(collidedToy.gameObject);
            }
        }
    }


}
