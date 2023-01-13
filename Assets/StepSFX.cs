using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSFX : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Step()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
