using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpeechBubble : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        if(image == null)
        {
            image = this.gameObject.GetComponent<Image>();
        }
        //Events.OnEmptyMotivation.AddListener(ShowSpeechBubble);
        Events.OnSceneChange.AddListener(OnSceneChange);
    }

    // Start is called before the first frame update

    public void ShowSpeechBubble(bool state)
    {
        this.gameObject.SetActive(state);


        Debug.Log("Low motivation" + this.gameObject.transform.parent.gameObject.transform.parent.name);
    }

    public void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        //Events.OnEmptyMotivation.RemoveListener(ShowSpeechBubble);
    }

    private void OnDestroy()
    {
        OnSceneChange();
    }

}
