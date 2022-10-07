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
       
    }

    private void OnEnable()
    {
        UpdateTimeRemaining();
    }
    // Start is called before the first frame update
    void Start()
    {
        minigameTimer = SingletonManager.Get<MiniGameTimer>();
        timeElapsedText = this.GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTimeRemaining()
    {
        Assert.IsNotNull(minigameTimer);
        timeElapsedText.text = minigameTimer.GetTimeRemaining().ToString("0") + " secs";
    }
   
}
