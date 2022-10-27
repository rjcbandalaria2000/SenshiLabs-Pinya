using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTimePeriod : MonoBehaviour
{
    public TextMeshProUGUI timePeriodText;
    public DayCycle dayCycle;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dayCycle = SingletonManager.Get<DayCycle>();
        timePeriodText = this.GetComponent<TextMeshProUGUI>();
        Events.OnSceneChange.AddListener(OnSceneChange);
        Events.OnChangeTimePeriod.AddListener(UpdateTimePeriod);
    }

    public void UpdateTimePeriod()
    {
        if(dayCycle == null) { return; }
        if(timePeriodText == null) { return; }
        timePeriodText.text = dayCycle.timePeriod.ToString();
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnChangeTimePeriod.RemoveListener(UpdateTimePeriod);
    }

   
}
