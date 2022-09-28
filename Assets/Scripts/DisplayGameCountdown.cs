using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGameCountdown : MonoBehaviour
{
    public TextMeshProUGUI gameCountdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateCountdownTimer(float time)
    {
        gameCountdownText.text = time.ToString("0"); 
    }

}
