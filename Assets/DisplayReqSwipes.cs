using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DisplayReqSwipes : MonoBehaviour
{
    public TextMeshProUGUI SwipesText;

    private GetWaterManager getWaterManager;
    // Start is called before the first frame update
    void Start()
    {
        SwipesText = this.GetComponent<TextMeshProUGUI>();
        getWaterManager = SingletonManager.Get<GetWaterManager>();
        UpdateSwipes();
    }

    public void UpdateSwipes() 
    { 
        Assert.IsNotNull(SwipesText, "Swipes text is null");
        Assert.IsNotNull(getWaterManager, "Get water manager is null");
        SwipesText.text = getWaterManager.RequiredNumSwipes.ToString();
    }

    public void OnSceneChange()
    {

    }
}
