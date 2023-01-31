using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MinigameObject : MonoBehaviour
{
    [Header("Setup Values")]
    protected string        id;
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
   // public ParticleSystem glow;
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

            if (completeState != null)
            {
                completeState.SetActive(false);
            }
        }
        else
        {
          
            if (uncompleteState != null)
            {
                uncompleteState.SetActive(false);
            }
    

            if (completeState != null)
            {
                completeState.SetActive(true);
              //  glow.Play();
                Debug.Log("FinishTask");
            }
             

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
      //  Debug.Log("End Interact");
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

    public virtual void DeactivateUnfinishState()
    {
        uncompleteState.SetActive(false);
    }

    public bool CheckIfPrerequisiteFinished()
    {
        return false;
    }

    public virtual void StopInteractRoutine()
    {

    }

    public virtual void OnSceneChange()
    {

    }

    public void ShakeScreen()
    {
        Camera camera = Camera.main;

        camera.DOShakePosition(0.8f, 2, 5, 90, true).WaitForCompletion();
    }
}
