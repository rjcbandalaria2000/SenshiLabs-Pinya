using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class DisplayDustCount : MonoBehaviour
{
    public TextMeshProUGUI DustCountText;

    private CleanTheHouseManager cleanTheHouseManager; 

    // Start is called before the first frame update
    void Start()
    {
        DustCountText = this.GetComponent<TextMeshProUGUI>();
        cleanTheHouseManager = SingletonManager.Get<CleanTheHouseManager>();
        Events.OnObjectiveUpdate.AddListener(UpdateCounter);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateCounter()
    {
        Assert.IsNotNull(DustCountText, "Text for dust count is not set or is null");
        Assert.IsNotNull(cleanTheHouseManager, "Clean The house manager is null");
        DustCountText.text = cleanTheHouseManager.GetRemainingDust().ToString();
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdateCounter);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

    
}
