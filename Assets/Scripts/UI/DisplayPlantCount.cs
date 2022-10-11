using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlantCount : MonoBehaviour
{
    public WaterThePlantsManager waterThePlantsManager;
    public TextMeshProUGUI plantCountText;

    private void Awake()
    {
        Events.OnObjectiveUpdate.AddListener(UpdatePlantCount);
        Events.OnSceneChange.AddListener(OnSceneChange);
        
        plantCountText = this.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterThePlantsManager = SingletonManager.Get<WaterThePlantsManager>();
    }

    public void UpdatePlantCount()
    {
        if(waterThePlantsManager == null) { return; }
        if (plantCountText == null) { return; }
        plantCountText.text = waterThePlantsManager.GetRemainingPlants().ToString("0");
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdatePlantCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
