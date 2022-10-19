using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [Header("Setup")]
    public int          RequiredIngredientCount;
    public int          CurrentIngredientCount;
    public float        CookingSpeed;

    [Header("States")]
    public bool         AreIngredientsAdded;
    public bool         IsCooked;
    public GameObject   unCookedImage;
    public GameObject   midCookedImage;
    public GameObject   cookedImage;
    public GameObject   potCover;
    
    [Header("Panels")]
    public GameObject   TempChoices;

    // Start is called before the first frame update
    void Start()
    {
        TempChoices.SetActive(false);
        Events.OnIngredientPlaced.Invoke();
        Events.OnPrepStage.Invoke();
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
                Events.OnIngredientPlaced.Invoke();
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
            Events.OnCookingStage.Invoke();
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

    public void DeactivateTempChoice()
    {
        if (TempChoices == null) { return; }
        TempChoices.SetActive(false);
    }

    public void ShowCookingStage(int stageIndex)
    {
        if(unCookedImage == null) { return; }
        if(midCookedImage == null) { return; }
        if(potCover == null) { return;} 
        if(cookedImage == null) { return; }
        switch (stageIndex)
        {
            case 0:
                unCookedImage.SetActive(false);
                midCookedImage.SetActive(false);
                cookedImage.SetActive(false);
                potCover.SetActive(true);
                break;
            case 1:
                unCookedImage.SetActive(true);
                midCookedImage.SetActive(false);
                cookedImage.SetActive(false);
                potCover.SetActive(true);
                break;
            case 2:
                unCookedImage.SetActive(true);
                midCookedImage.SetActive(true);
                cookedImage.SetActive(false);
                potCover.SetActive(true);
                break;
            case 3:
                unCookedImage.SetActive(true);
                midCookedImage.SetActive(true);
                cookedImage.SetActive(true);
                potCover.SetActive(false);
                break;
            default:
                unCookedImage.SetActive(false);
                midCookedImage.SetActive(false);
                cookedImage.SetActive(false);
                potCover.SetActive(true);
                break;
        }
    }

    public int GetRemainingIngredients()
    {
        return RequiredIngredientCount - CurrentIngredientCount;
    }

}
