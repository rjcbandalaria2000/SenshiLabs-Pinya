using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWaterBucket : MonoBehaviour
{
    [Header("Values")]
    public float        waterAmount = 0;
    public float        maxWater = 5;

    [Header("Fill Speed")]
    public float        fillSpeed = 1;
    public float        fillAmount = 0.1f;

    private Coroutine   fillBucketRoutine;

    // Start is called before the first frame update
    void Start()
    {
        //StartFillingBucket();
    }

    public void StartFillingBucket()
    {
        fillBucketRoutine = StartCoroutine(FillTheBucket());
    }

    IEnumerator FillTheBucket()
    {
        while(waterAmount < maxWater)
        {
            yield return new WaitForSeconds(1/ fillSpeed);
            waterAmount += fillAmount;
            Events.OnWaterFilling.Invoke();
        }
        yield return null;
    }

    public void StopFillingBucket()
    {
        if (fillBucketRoutine == null) { return; }
        StopCoroutine(fillBucketRoutine);
    }
    
    public void ResetWaterBucket()
    {
        waterAmount = 0;
        Events.OnWaterFilling.Invoke();
    }

    //public float GetWaterAmount()
    //{
    //    return waterAmount;
    //}
}