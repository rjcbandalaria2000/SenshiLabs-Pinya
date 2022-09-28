using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
                countdownImages[i].transform.DOShakeScale(0.5f, 0.1f, 3, 0, true);
                countdownImages[i].transform.DOScale(1, 1).WaitForCompletion();
            }
        }
    }

    

}
