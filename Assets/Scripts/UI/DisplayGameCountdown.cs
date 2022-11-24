using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DisplayGameCountdown : MonoBehaviour
{
    public TextMeshProUGUI gameCountdownText;
    
    public List<GameObject> countdownImages;

    public List<AudioClip> sfx;

    SFXManager sFXManager;

     int index;

    // Start is called before the first frame update
    private void Awake()
    {
        sFXManager = GetComponent<SFXManager>();
    }
    void Start()
    {
        //   countdownImages = new List<GameObject>();
        for (int i = 0; i < countdownImages.Count; i++)
        {
            countdownImages[i].SetActive(false);
        }
    }

    public void UpdateCountdownTimer(float time)
    {
        gameCountdownText.text = time.ToString("0"); 
    }

    public void PlaySFX()
    {
      
     //   sFXManager.StopMusic();
       
    }
    public void UpdateCountdownSprites(float time)
    {
       // PlaySFX();
       
       
        for (int i = 0; i < countdownImages.Count; i++)
        {
            //countdownImages[i].SetActive(false);
            
            if (time == i)
            {
                //  index = i;
                if (!countdownImages[i].activeSelf)
                {
                    countdownImages[i].SetActive(true);
                    countdownImages[i].transform.DOShakeScale(0.5f, 0.1f, 3, 0, true);
                    countdownImages[i].transform.DOScale(1, 1).WaitForCompletion();
                    sFXManager.PlaySFX(sfx[i]);
                    Debug.Log("PlaySFX");
                }
                
            }
            else
            {
                countdownImages[i].SetActive(false);
            }

           
        }


    }


    private void OnDisable()
    {
    //    index = 0;
    }

}
