using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayTimeElapsed : MonoBehaviour
{
    public MiniGameTimer minigameTimer;
    public TextMeshProUGUI timeElapsedText;

    private void Awake()
    {
        minigameTimer = SingletonManager.Get<MiniGameTimer>();
        timeElapsedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateTimeElapsed();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateTimeElapsed()
    {
        Assert.IsNotNull(minigameTimer);
        timeElapsedText.text = minigameTimer.GetTimeElapsed().ToString("0") + " secs";
    }
   
}
