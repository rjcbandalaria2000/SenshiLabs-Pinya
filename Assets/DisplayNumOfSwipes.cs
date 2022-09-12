using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DisplayNumOfSwipes : MonoBehaviour
{
    public TextMeshProUGUI SwipesText;
    public GameObject Well;
    private GetWaterManager getWaterManager;
    private WaterWell waterWell;
    // Start is called before the first frame update
    void Start()
    {
        SwipesText = this.GetComponent<TextMeshProUGUI>();
        Assert.IsNotNull(Well, "Well object is null");
        waterWell = Well.GetComponent<WaterWell>();
        Events.OnObjectiveUpdate.AddListener(UpdateSwipes);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateSwipes()
    {
        Assert.IsNotNull(SwipesText, "Swipes text is null");
        Assert.IsNotNull(waterWell, "Well is null");
        SwipesText.text = waterWell.GetSwipeDown().ToString();
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdateSwipes);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

}
