using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWaterAmount : MonoBehaviour
{
    public List<GameObject> bucketsUI;
    public WaterWell well;

    // Start is called before the first frame update
    void Start()
    {
        if (well)
        {
            well.OnBucketFilled.AddListener(UpdateWaterAmount);
        }
        else
        {
            well = SingletonManager.Get<GetWaterManager>().wateringWell.GetComponent<WaterWell>();
            well.OnBucketFilled.AddListener(UpdateWaterAmount);
        }
        Events.OnSceneChange.AddListener(OnSceneChange);
        if(bucketsUI.Count > 0)
        {
            for(int i = 0; i < bucketsUI.Count; i++)
            {
                DisplayBucketFill displayBucket = bucketsUI[i].GetComponent<DisplayBucketFill>();
                if (displayBucket)
                {
                    displayBucket.UpdateWaterFill(0);
                }
            }
        }
    }

    public void UpdateWaterAmount(int index, float fillAmount)
    {
        if(index >= bucketsUI.Count) { return; }
        if(index < 0) 
        {
            index = 0;
        }
        DisplayBucketFill displayBucketFill = bucketsUI[index].GetComponent<DisplayBucketFill>();
        if (displayBucketFill)
        {
            displayBucketFill.UpdateWaterFill(fillAmount);
        }
    }

    public void OnSceneChange()
    {
        well.OnBucketFilled.RemoveListener(UpdateWaterAmount);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
  
}
