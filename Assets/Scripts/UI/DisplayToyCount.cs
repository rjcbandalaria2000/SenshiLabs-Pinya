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

    private void Awake()
    {
        ToyCountText = this.GetComponent<TextMeshProUGUI>();
        cleanTheHouseManager = SingletonManager.Get<CleanTheHouseManager>();
        Events.OnObjectiveUpdate.AddListener(UpdateCounter);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void UpdateCounter()
    {
        Assert.IsNotNull(ToyCountText, "Text for dust count is not set or is null");
        Assert.IsNotNull(cleanTheHouseManager, "Clean The house manager is null");
        ToyCountText.text = cleanTheHouseManager.GetRemainingToys().ToString();
        Debug.Log("Update Toycount");
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdateCounter);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
