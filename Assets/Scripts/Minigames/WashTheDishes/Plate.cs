using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Header("States")]
    public bool         swipedRight;
    public bool         swipedLeft;
    public bool         IsClean = false;
    public bool         CanClean;

    [Header("Values")]
    public int          SwipeRequired;

    [Header("Mouse Sweep Acceptance")]
    [Range(0f, -1f)]
    public float        SwipeLeftAccept = -0.5f;
    [Range(0f, 1f)]
    public float        SwipeRightAccept = 0.5f;

    [Header("Models")]
    public GameObject   CleanPlateModel;
    public GameObject   DirtyPlateModel;
   

    private int         swipeCounter;

    private Sponge      sponge;
    private Coroutine   spongeInteractRoutine;


    public SFXManager sFX;
    public AudioClip audioClip;

    [Header("UX")]
    public ParticleSystem particle;


    // Start is called before the first frame update
    void Start()
    {
        spongeInteractRoutine = null;
        ChangeModel();
        CanClean = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sponge collidedSponge = collision.gameObject.GetComponent<Sponge>();
        if (collidedSponge)
        {
            sponge = collidedSponge;
            if (CanClean)
            {
                StartSpongeInteract();
                sFX.PlaySFX(audioClip);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Sponge collidedSponge = collision.gameObject.GetComponent<Sponge>();
        if (collidedSponge)
        {
            sponge = null;
            StopSpongeInteract();
            this.sFX.StopMusic();
        }
    }

    public void StartSpongeInteract()
    {
        spongeInteractRoutine = StartCoroutine(SpongeInteract());
    }

    public void StopSpongeInteract()
    {
        if(spongeInteractRoutine != null)
        {
            StopCoroutine(spongeInteractRoutine);
        }
        
    }

    public void ChangeModel()
    {
        //Changes model from dirty plate to clean plate vice versa 
        if (IsClean)
        {
            
            CleanPlateModel.SetActive(true);
            DirtyPlateModel.SetActive(false);
        }
        else
        {
            CleanPlateModel.SetActive(false);
            DirtyPlateModel.SetActive(true);
        }
    }

    IEnumerator SpongeInteract()
    {

        while (true)
        {
            if (sponge == null) { break; }
            // The position of the plate and the position of the sponge
            Vector2 spongePosition = sponge.transform.position + this.gameObject.transform.position;//sponge.gameObject.transform.position - (Vector3)initialPosition;

            if (spongePosition.normalized.x < SwipeLeftAccept) // if sponge swiped left 
            {
                swipedLeft = true;
            }
            if (spongePosition.normalized.x > SwipeRightAccept) // if sponge swiped right
            {
                swipedRight = true;
            }
            if (swipedRight && swipedLeft) // if the player both reached both ends
            {
                swipeCounter++;
                swipedLeft = false;
                swipedRight = false;
            }
            if (swipeCounter >= SwipeRequired)
            {
                if (!IsClean)
                {
                    IsClean = true;
                    particle.Play();
                    ChangeModel();
                    Events.OnPlateCleaned.Invoke();
       
                }
                
            }
            Debug.Log("XCoordinates: " + spongePosition.normalized.x);
            yield return null;
        }
        
        
    }

  
}
