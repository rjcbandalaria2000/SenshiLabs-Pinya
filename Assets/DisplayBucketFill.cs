using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayBucketFill : MonoBehaviour
{
    public FillWaterBucket  waterBucket;
    public GameObject       well;
    public Image            waterBucketFill;

    [Header("Tween Animation")]
    public float            duration = 1f;
    public float            strength = 10f;

    [Header("VFX")]
    public GameObject       sparkle;
    public Sequence         shakeSequence;

    private WaterWell       waterWell;

    // Start is called before the first frame update
    void Start()
    {
        Events.OnWaterFilling.AddListener(UpdateWaterFill);
        Events.OnSceneChange.AddListener(OnSceneChange);
        Events.OnBucketSuccess.AddListener(PlaySuccessEffect);
        Events.OnBucketFailed.AddListener(PlayFailedEffect);
        if(well == null)
        {
            well = SingletonManager.Get<GetWaterManager>().wateringWell;
        }
        waterWell = well.GetComponent<WaterWell>();
    }

    public void UpdateWaterFill()
    {
        if(waterBucketFill == null) { return; }
        if(waterBucket == null) { return; }
        //waterBucketFill.fillAmount = waterBucket.GetNormalizedWaterAmount();
        waterBucketFill.DOFillAmount(waterBucket.GetNormalizedWaterAmount(), duration) ;
    }

    public void UpdateWaterFill(float amount)
    {
        if(waterBucketFill == null) { return; }
        waterBucketFill.DOFillAmount(amount, duration);
    }
    
    public void OnSceneChange()
    {
        Events.OnWaterFilling.RemoveListener(UpdateWaterFill);
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnBucketSuccess.RemoveListener(PlaySuccessEffect);
        Events.OnBucketFailed.RemoveListener(PlayFailedEffect);
    }

    public void PlaySuccessEffect()
    {
        if (sparkle == null) { return; }
        if (!sparkle.activeSelf)
        {
            sparkle.SetActive(true);
        }
        ParticleSystem particle = sparkle.GetComponent<ParticleSystem>();
        if (particle)
        {
            particle.Play();
            if (!particle.isPlaying)
            {
                sparkle.SetActive(false);
            }
        }
    }

    public void PlayFailedEffect()
    {
        this.gameObject.transform.DOShakePosition(0.5f, new Vector3(strength,0,0), 10, 0, false, false  );
    }

}
