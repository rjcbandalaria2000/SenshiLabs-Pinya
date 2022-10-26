using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);
    }

    public void StopMusic()
    {
        source.Stop();
    }
}
