using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    [Header("Setup Values")]
    public string           minigameName;
    public int              motivationCost;
    
    public Interactable     interactable;
    public bool             isInteracted;
    public bool             hasCompleted;
    public string           minigameScene;

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
        //interactable = this.GetComponent<Interactable>();

        //if (interactable)
        //{
        //    //Listen to the interactable event and proceed to interact with the player
        //    Events.OnInteract.AddListener(Interact);
        //    Events.OnFinishInteract.AddListener(EndInteract);
            
        //}
        //sceneChange = this.gameObject.GetComponent<SceneChange>();
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
