using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCookingCounter : MonoBehaviour
{
    public TextMeshProUGUI cookingCountText;
    public GameObject temperatureMeter;

    private TemperatureControl tempMeter; 

    // Start is called before the first frame update
    void Start()
    {
        if(temperatureMeter != null)
        {
            tempMeter = temperatureMeter.GetComponent<TemperatureControl>();
        }
        cookingCountText = this.GetComponent<TextMeshProUGUI>();
        Events.OnCookingButtonPressed.AddListener(UpdateCookingCounter);
        Events.OnSceneChange.AddListener(OnSceneChange);
        UpdateCookingCounter();
    }

    public void UpdateCookingCounter()
    {
        if (tempMeter == null) { return; }
        cookingCountText.text = tempMeter.GetRemainingCookingCounter().ToString("0");
    }

    public void OnSceneChange()
    {
        Events.OnCookingButtonPressed.RemoveListener(UpdateCookingCounter);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
}
