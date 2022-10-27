using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwipeArrow : MonoBehaviour
{
    public GameObject upArrowImage;
    public GameObject downArrowImage;

    private void Awake()
    {
        Events.OnBucketDrop.AddListener(ActivateDownArrow);
        Events.OnBucketRetrieve.AddListener(ActivateUpArrow);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateArrow(string image)
    {
        upArrowImage.SetActive(upArrowImage.Equals(image));
        downArrowImage.SetActive(downArrowImage.Equals(image));
    }

    public void ActivateUpArrow()
    {
        upArrowImage.SetActive(true);
        downArrowImage.SetActive(false);
    }
    public void ActivateDownArrow()
    {
        upArrowImage.SetActive(false);
        downArrowImage.SetActive(true);
    }

    public void DeactivateArrows()
    {
        upArrowImage.SetActive(false);
        downArrowImage.SetActive(false);
    }

    public void OnSceneChange()
    {
        Events.OnBucketDrop.RemoveListener(ActivateDownArrow);
        Events.OnBucketRetrieve.RemoveListener(ActivateUpArrow);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }

}
