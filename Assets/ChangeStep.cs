using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStep : MonoBehaviour
{
    // Start is called before the first frame update
    public StepSFX stepSFX;
    public AudioClip replaceAudioClip;
    private void Awake()
    {
        stepSFX = GameObject.FindGameObjectWithTag("Step").GetComponent<StepSFX>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stepSFX.audioClip = replaceAudioClip;
        }
    }
}
