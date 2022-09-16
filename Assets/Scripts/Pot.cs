using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("Setup")]
    public int      RequiredIngredientCount;
    public int      CurrentIngredientCount;
    public float    CookingSpeed;

    [Header("States")]
    public bool     AreIngredientsAdded;
    public bool     ReadyToCook;

    [Header("Panels")]
    public GameObject TempChoices;

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
                CheckAllIngredients();
                collidedIngredient.gameObject.SetActive(false);
            }
        }
    }

    public void CheckAllIngredients()
    {
        if(CurrentIngredientCount >= RequiredIngredientCount)
        {
            AreIngredientsAdded = true;
            if(TempChoices == null) { return; }
            ActivateTempChoice();
        }
    }

    public void SetCookingSpeed(float speed)
    {
        CookingSpeed = speed;
        TempChoices.SetActive(false);
    }

    private void OnMouseDown()
    {
        
    }

    public void ActivateTempChoice()
    {
        if (TempChoices == null) { return; }
        TempChoices.SetActive(true);
        TemperatureControl tempControl = TempChoices.GetComponent<TemperatureControl>();
        if (tempControl)
        {
            tempControl.StartMoveTracker();
        }
    }

}
