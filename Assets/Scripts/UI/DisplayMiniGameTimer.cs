using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayMiniGameTimer : MonoBehaviour
{
    public TextMeshProUGUI miniGameTimer_UI;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<MiniGameTimer>() != null)
        {
            miniGameTimer_UI.text = SingletonManager.Get<MiniGameTimer>().timer.ToString();
            Events.OnDisplayMinigameTime.AddListener(updateMiniGameTimer);
        }
        else
        {
            Debug.Log("MiniGameTimer doesnt exist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMiniGameTimer()
    {
        if (SingletonManager.Get<MiniGameTimer>() != null)
        {
            miniGameTimer_UI.text = SingletonManager.Get<MiniGameTimer>().timer.ToString();
        }
        else
        {
            Debug.Log("MiniGameTimer doesnt exist");
        }
    }
}
