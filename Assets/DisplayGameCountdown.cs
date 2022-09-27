using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGameCountdown : MonoBehaviour
{
    public TextMeshProUGUI gameCountdownText;
    
    public List<GameObject> countdownImages;

    // Start is called before the first frame update
    void Start()
    {
     //   countdownImages = new List<GameObject>();
    }

    public void UpdateCountdownTimer(float time)
    {
        gameCountdownText.text = time.ToString("0"); 
    }

    public void UpdateCountdownSprites(float time)
    {

        for (int i = 0; i < countdownImages.Count; i++)
        {
            countdownImages[i].SetActive(false);

            if (time == i)
            {
                countdownImages[i].SetActive(true);
            }
        }
    }
}
