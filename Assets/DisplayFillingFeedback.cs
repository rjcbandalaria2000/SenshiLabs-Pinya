using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFillingFeedback : MonoBehaviour
{
    public FillWaterBucket fillWaterBucket;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowFeedbackText()
    {
        if (!text.enabled)
        {
            text.enabled = true;

        }
    }
}
