using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlateCount : MonoBehaviour
{
    public WashTheDishesManager washTheDishesManager;
    public TextMeshProUGUI      plateCountText;

    private void Awake()
    {
        Events.OnObjectiveUpdate.AddListener(UpdatePlateCount);
        Events.OnSceneChange.AddListener(OnSceneChange);
        plateCountText = this.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void UpdatePlateCount()
    {
        if(plateCountText == null) { return; }
        if(washTheDishesManager == null) { return; }
        plateCountText.text = washTheDishesManager.GetRemainingDirtyPlates().ToString("0");
    }

    public void OnSceneChange()
    {
        Events.OnObjectiveUpdate.RemoveListener(UpdatePlateCount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
