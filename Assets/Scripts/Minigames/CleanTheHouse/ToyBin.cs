using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ToyBin : MonoBehaviour
{
    public SFXManager sFX;
    public AudioClip clip;
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
            if (collidedToy.isPickedUp && !collidedToy.isHolding)
            {
                Debug.Log("Accept toy");
                sFX.PlaySFX(clip);
                SingletonManager.Get<CleanTheHouseManager>().AddTrashThrown(1);
                Destroy(collidedToy.gameObject);
                Shake();
            }
        }
    }


    public void Shake()
    {
        gameObject.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f, 1, 1);
    }

}
