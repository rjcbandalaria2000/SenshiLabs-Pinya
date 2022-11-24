using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySpeechBubble : MonoBehaviour
{
    public GameObject parent;

    private MinigameObject parentMinigameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (parent)
        {
            parentMinigameObject = parent.GetComponent<MinigameObject>();
            parentMinigameObject.onPlayerEnter.AddListener(ShowSpeechBubble);
            parentMinigameObject.onPlayerExit.AddListener(HideSpeechBubble);
        }
        Events.OnSceneChange.AddListener(OnSceneChange);
        this.gameObject.SetActive(false);
    }

    public void ShowSpeechBubble() 
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void HideSpeechBubble()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OnSceneChange()
    {
        if (parent)
        {
            parentMinigameObject.onPlayerEnter.RemoveListener(ShowSpeechBubble);
            parentMinigameObject.onPlayerExit.RemoveListener(HideSpeechBubble);
        }
        
        Events.OnSceneChange.RemoveListener(OnSceneChange);
    }
   
}
