using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class DisplayTimeRemaining : MonoBehaviour
{
    public MiniGameTimer    minigameTimer;
    public TextMeshProUGUI  timeElapsedText;

    private void Awake()
    {
        minigameTimer = SingletonManager.Get<MiniGameTimer>();
        timeElapsedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateTimeRemaining();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateTimeRemaining()
    {
        Assert.IsNotNull(minigameTimer);
        timeElapsedText.text = minigameTimer.GetTimeRemaining().ToString("0") + " secs";
    }
   
}
