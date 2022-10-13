using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRemainingIngredients : MonoBehaviour
{
    public GameObject potGO;
    public TextMeshProUGUI ingredientText;

    private Pot pot;

    // Start is called before the first frame update
    void Start()
    {
        if(potGO == null)
        {
            potGO = SingletonManager.Get<ImHungryManager>().Pot;
        }
        if (potGO != null) 
        {
            pot = potGO.GetComponent<Pot>();
        }
        ingredientText = this.GetComponent<TextMeshProUGUI>();

        Events.OnIngredientPlaced.AddListener(UpdateRemainingIngredients);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateRemainingIngredients()
    {
        if (pot == null) { return; }
        ingredientText.text = pot.GetRemainingIngredients().ToString("0");
    }

    public void OnSceneChange()
    {
        Events.OnIngredientPlaced.RemoveListener(UpdateRemainingIngredients);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

}
