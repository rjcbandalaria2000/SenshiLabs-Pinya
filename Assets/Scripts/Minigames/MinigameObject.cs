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
    protected Coroutine     interactRoutine;
    private void Awake()
    {
       
    }
    private void Start()
    {

        
    }

    public virtual void Initialize()
    {

    }

    public virtual void Interact(GameObject player = null)
    {
      
    }

    public virtual void EndInteract(GameObject player = null)
    {
        Debug.Log("End Interact");
    }

    public virtual void JumpToMiniGame()
    {

    }

    public virtual void StartInteractRoutine()
    {

    }

    public virtual IEnumerator InteractCoroutine(GameObject player = null)
    {
        yield return null; 
    }
}
