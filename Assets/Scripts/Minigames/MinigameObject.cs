using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    [Header("Setup Values")]
    public int              MotivationCost;
    
    public Interactable     Interactable;
    public bool             isInteracted;
    public bool             HasCompleted;
    public string           MinigameScene;

    protected SceneChange   sceneChange;
    protected PlayerData    playerData;
    //protected Coroutine     interactRoutine;
    private void Awake()
    {
       
    }
    private void Start()
    {

        
    }

    public virtual void Initialize()
    {
        Interactable = this.GetComponent<Interactable>();

        if (Interactable)
        {
            //Listen to the interactable event and proceed to interact with the player
            Events.OnInteract.AddListener(Interact);
            Events.OnFinishInteract.AddListener(EndInteract);
            
        }
        sceneChange = this.gameObject.GetComponent<SceneChange>();
    }

    public virtual void Interact(GameObject player = null)
    {
        //When player is interacting with the object 
        // For Scene change, always start at the persistent scene
        
    }

    public virtual void EndInteract(GameObject player = null)
    {
        // When player is out of range or leaves
        Debug.Log("End Interact");
    }

    public virtual void JumpToMiniGame()
    {

    }
    public virtual IEnumerator InteractCoroutine(GameObject player = null)
    {
        yield return null; 
    }
}
