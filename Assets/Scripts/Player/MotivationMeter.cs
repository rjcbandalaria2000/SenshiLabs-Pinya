using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class MotivationMeter : MonoBehaviour
{
    [Header("Values")]
    public float        MotivationAmount;
    public float        MaxMotivation;

    [Header("Threshold Values")]
    public float        minMotivationAmount = 30f;

    public Slider       thresholdSlider;

    public float        coolDown;
    bool                isThreshold;

    public List<GameObject> bubbleSpeeches;

    

    // Start is called before the first frame update
    void Start()
    {
        if (SingletonManager.Get<PlayerData>().hasSaved)
        {
            MotivationAmount = SingletonManager.Get<PlayerData>().storedMotivationData;
        }
        else
        {
            InitializeMeter();
        }
        CheckMotivationalMeter();
        Events.OnChangeMeter.Invoke();
    }

    public void InitializeMeter() {

        MotivationAmount = MaxMotivation;
        //Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void IncreaseMotivation(float value)
    {
        if(MotivationAmount < MaxMotivation)
        {
            MotivationAmount += value;
        }
        else
        {
            Debug.Log("Motivation is maxed out");
        }
        MotivationAmount = Mathf.Clamp(MotivationAmount, 0, MaxMotivation);
        CheckMotivationalMeter();
        Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void DecreaseMotivation(float value)
    {
        if(MotivationAmount > 0)
        {
            MotivationAmount -= value;  
        }
        else
        {
            Debug.Log("Motivation is already at 0");

        }
        CheckMotivationalMeter();
        Events.OnChangeMeter.Invoke();
        //EvtChangeMeter.Invoke();
    }

    public void CheckMotivationalMeter()
    {
        if(MotivationAmount <= minMotivationAmount)
        {
            if(bubbleSpeeches != null)
            {
                for(int i = 0; i < bubbleSpeeches.Count; i++)
                {
                    bubbleSpeeches[i].SetActive(true);
                }
            }

            //Events.OnEmptyMotivation.Invoke(true);
            isThreshold = true;
            ActivateThreshold();
        
        }
        else
        {
            if (bubbleSpeeches != null)
            {
                for (int i = 0; i < bubbleSpeeches.Count; i++)
                {
                    bubbleSpeeches[i].SetActive(false);
                }
            }
            //Events.OnEmptyMotivation.Invoke(false);
            isThreshold = false;
        //    ShakeScreen();
        }
    }

    public IEnumerator PulsateObj()
    {
      while (isThreshold)
        {
         //   Debug.Log("animatio");
            thresholdSlider.gameObject.transform.DOScale(0.6199f, 0.3f).SetEase(Ease.OutBounce);
            // overlayGO.transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(1f);
            thresholdSlider.gameObject.transform.DOScale(3f, 0.3f).SetEase(Ease.OutBounce);
        }

        thresholdSlider.gameObject.transform.DOScale(0.6199f, 0.3f).SetEase(Ease.OutBounce);
    }

    public void ActivateThreshold()
    {
        StartCoroutine(PulsateObj());
      
    }

 
    
}
