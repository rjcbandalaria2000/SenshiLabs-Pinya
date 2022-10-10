using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWaterBucket : MonoBehaviour
{
    public float        waterAmount = 0;
    public float        maxWater = 5;
    public float        fillSpeed = 1;
    public float        fillAmount = 0.1f;

    private Coroutine   fillBucketRoutine;

    // Start is called before the first frame update
    void Start()
    {
        StartFillingBucket();
    }

    public void StartFillingBucket()
    {
        fillBucketRoutine = StartCoroutine(FillTheBucket());
    }

    IEnumerator FillTheBucket()
    {
        while(waterAmount < maxWater)
        {
            yield return new WaitForSeconds(fillSpeed);
            waterAmount += fillAmount;
        }
        yield return null;
    }
   
}
