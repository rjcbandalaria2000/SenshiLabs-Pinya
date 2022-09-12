using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Assertions;

public class DisplayToyCount : MonoBehaviour
{
    public TextMeshProUGUI ToyCountText;

    private CleanTheHouseManager cleanTheHouseManager;

    // Start is called before the first frame update
    void Start()
    {
        ToyCountText = this.GetComponent<TextMeshProUGUI>();
        cleanTheHouseManager = SingletonManager.Get<CleanTheHouseManager>();
        Events.OnObjectiveUpdate.AddListener(UpdateCounter);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    public void UpdateCounter()
    {
        Assert.IsNotNull(ToyCountText, "Text for dust count is not set or is null");
        Assert.IsNotNull(cleanTheHouseManager, "Clean The house manager is null");
        ToyCountText.text = cleanTheHouseManager.GetRemainingToys().ToString();
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdateCounter);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
