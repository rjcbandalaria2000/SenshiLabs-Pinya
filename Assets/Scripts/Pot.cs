using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("Setup")]
    public int RequiredIngredientCount;
    public int CurrentIngredientCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Ingredient collidedIngredient = collision.gameObject.GetComponent<Ingredient>();
        if (collidedIngredient)
        {
            Debug.Log("Collided with ingredient");
            if(collidedIngredient.IsPickedUp && !collidedIngredient.IsHolding)
            {
                Debug.Log("Accept Ingredient");
                CurrentIngredientCount++;
                collidedIngredient.gameObject.SetActive(false);
            }
        }
    }

}
