using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    public Interactable     Interactable;
    public bool             isInteracted;
    public string           MinigameScene;

    protected SceneChange sceneChange;

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
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (sceneChange)
        //    {  
        //        //Remove all listeners because when the scene changes it will destroy the scene but will try to access the old active scripts
        //        Events.OnSceneChange.Invoke();
        //        Events.OnInteract.RemoveListener(Interact);
        //        Events.OnFinishInteract.RemoveListener(EndInteract);
                
        //        sceneChange.OnChangeScene(MinigameScene);
                
        //    }
        //    else
        //    {
        //        Debug.Log("No Scene change");
        //    }
        //}
        
       // Debug.Log("Interacted with " + player.name);
    }

    public virtual void EndInteract(GameObject player = null)
    {
        // When player is out of range or leaves
        Debug.Log("End Interact");
    }

    public virtual void JumpToMiniGame()
    {

    }
}
