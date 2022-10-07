using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwipeArrow : MonoBehaviour
{
    public GameObject upArrowImage;
    public GameObject downArrowImage;
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

}
