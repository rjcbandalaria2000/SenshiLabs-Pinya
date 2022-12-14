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

    public List<MinigameObject> preRequisiteTasks;

    protected SceneChange   sceneChange;
    protected PlayerData    playerData;
    protected Coroutine     interactRoutine;

    public GameObject uncompleteState;
    public GameObject completeState;
    private void Awake()
    {
       
    }
    private void Start()
    {

        
    }
    private void OnEnable()
    {
        if (hasCompleted == false)
        {
            if (uncompleteState != null)
            {
                uncompleteState.SetActive(true);
            }

            if(completeState != null)
            {
                completeState.SetActive(false);
            }
        }
        else
        {
            if (uncompleteState != null)
                uncompleteState.SetActive(false);

            if (completeState != null)
               completeState.SetActive(true);
            
        }
    }

    private void OnDisable()
    {
        if (uncompleteState != null)
            uncompleteState.SetActive(false);
        if (completeState != null)
            completeState.SetActive(false);
        
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

    public virtual void deactivateUnfinishState()
    {
        uncompleteState.SetActive(false);
    }

    public bool CheckIfPrerequisiteFinished()
    {
        return false;
    }
}
