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
        }
        Events.OnEmptyMotivation.AddListener(ShowSpeechBubble);
        Events.OnSceneChange.AddListener(OnSceneChange);
        this.gameObject.SetActive(false);
    }

    public void ShowSpeechBubble(bool state) 
    {
        
            this.gameObject.SetActive(state);
        
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnEmptyMotivation.RemoveListener(ShowSpeechBubble);
    }
   
}
