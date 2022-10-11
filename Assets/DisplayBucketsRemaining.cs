using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBucketsRemaining : MonoBehaviour
{
    public WaterWell waterWell;
    public TextMeshProUGUI bucketsText;

    private void Awake()
    {
        bucketsText = this.GetComponent<TextMeshProUGUI>();
        if(waterWell == null)
        {
            waterWell = SingletonManager.Get<GetWaterManager>().wateringWell.GetComponent<WaterWell>();
        }
        Events.OnBucketUsed.AddListener(UpdateBucketsRemaining);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateBucketsRemaining()
    {
        if(waterWell == null) { return; }
        bucketsText.text = waterWell.availableBuckets.ToString("0");
    }

    public void OnSceneChange()
    {
        Events.OnBucketUsed.RemoveListener(UpdateBucketsRemaining);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

  
}
