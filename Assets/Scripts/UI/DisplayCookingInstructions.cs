using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCookingInstructions : MonoBehaviour
{
    public GameObject ingredientInstructionsUI;
    public GameObject cookingInstructionUI;

    private void Awake()
    {
        Events.OnPrepStage.AddListener(ActivateIngredientInstructions);
        Events.OnCookingStage.AddListener(ActivateCookingInstructions);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ActivateIngredientInstructions()
    {
        if(ingredientInstructionsUI == null) { return; }
        if(cookingInstructionUI == null) { return; }
        ingredientInstructionsUI.SetActive(true);
        cookingInstructionUI.SetActive(false);
    }

    public void ActivateCookingInstructions()
    {
        if (ingredientInstructionsUI == null) { return; }
        if (cookingInstructionUI == null) { return; }
        ingredientInstructionsUI.SetActive(false);
        cookingInstructionUI.SetActive(true);
    }

    public void OnSceneChange()
    {
        Events.OnPrepStage.RemoveListener(ActivateIngredientInstructions);
        Events.OnCookingStage.RemoveListener(ActivateCookingInstructions);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
