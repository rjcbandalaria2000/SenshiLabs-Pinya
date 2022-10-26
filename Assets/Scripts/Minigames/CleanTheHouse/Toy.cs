using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toy : MonoBehaviour
{
    [Header("States")]
    public bool         isHolding;
    public bool         isHoveredOver;
    public bool         isPickedUp;
    AudioSource audioSource;
    public AudioClip clip;

  //  public UnityEvent OnPickUp;
   // public UnityEvent OnPickDown;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPickedUp = false;
    }

    private void OnMouseDown()
    {
        isPickedUp = true;
      //  OnPickUp.Invoke();
    }

    private void OnMouseOver()
    {
        if (!isHoveredOver)
        {
            isHoveredOver = true;
        }
    }

    private void OnMouseExit()
    {
        if (isHoveredOver)
        {
            isHoveredOver = false;
        }
    }

    private void OnMouseDrag()
    {
        isHolding = true;
    }

    private void OnMouseUp()
    {
        isHolding = false;
        audioSource.PlayOneShot(clip);
        //OnPickDown.Invoke();
    }

}
