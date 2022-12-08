using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFillingFeedback : MonoBehaviour
{
    public FillWaterBucket fillWaterBucket;
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Events.OnWaterFilling.AddListener(ShowFeedbackText);
        Events.OnBucketUsed.AddListener(HideFeedbackText);
        Events.OnSceneChange.AddListener(OnSceneChange);
        if (text)
        {
            text.enabled = false;
        }
    }

    public void ShowFeedbackText()
    {
        if(text == null) { return; }
        if (!text.enabled)
        {
            text.enabled = true;
        }
    }

    public void HideFeedbackText()
    {
        if(text == null) { return; }
        if (text.enabled)
        {
            text.enabled = false;
        }
    }

    public void OnSceneChange()
    {
        Events.OnWaterFilling.RemoveListener(ShowFeedbackText);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnBucketUsed.RemoveListener(HideFeedbackText);
    }
}
