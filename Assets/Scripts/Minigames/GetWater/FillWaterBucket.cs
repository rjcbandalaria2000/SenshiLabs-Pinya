using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWaterBucket : MonoBehaviour
{
    [Header("Values")]
    public float waterAmount = 0;
    public float maxWater = 5;

    [Header("States")]
    public bool isFilling = false;

    [Header("Fill Speed")]
    public float fillSpeed = 1;
    public float fillAmount = 0.1f;

    private Coroutine fillBucketRoutine;

    private SFXManager sFX;

    [Header("VFX")]
    public GameObject sparkleEffect;
    //private Coroutine effectsRoutine;


    // Start is called before the first frame update
    void Start()
    {
        sFX = GetComponent<SFXManager>();
    }

    public void StartFillingBucket()
    {
        if (!isFilling)
        {
            isFilling = true;
            sFX.PlayMusic();
            fillBucketRoutine = StartCoroutine(FillTheBucket());
        }

    }

    IEnumerator FillTheBucket()
    {
        while (waterAmount < maxWater)
        {
            yield return new WaitForSeconds(1 / fillSpeed);
            waterAmount += fillAmount;
            Events.OnWaterFilling.Invoke();
        }
        yield return null;
    }

    public void StopFillingBucket()
    {
        //Play appropriate effects
        if(waterAmount >= maxWater) // if water is filled, play sparkle effect
        {
            Events.OnBucketSuccess.Invoke();
        }
        else // if bucket is not filled, shake the bucket
        {
            Events.OnBucketFailed.Invoke();
        }
        if (fillBucketRoutine == null) { return; }
        StopCoroutine(fillBucketRoutine);
        isFilling = false;

        
    }

    public void ResetWaterBucket()
    {
        sFX.StopMusic();
        waterAmount = 0;
        Events.OnWaterFilling.Invoke();
    }

    public float GetNormalizedWaterAmount()
    {
        return waterAmount / maxWater;
    }
}
