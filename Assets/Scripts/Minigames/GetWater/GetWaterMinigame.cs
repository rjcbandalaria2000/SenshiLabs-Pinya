using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWaterMinigame : MinigameObject
{
    private TransitionManager transitionManager;

    private void Awake()
    {
        Initialize();
    }

    public override void Initialize()
    {
        id = Constants.GET_WATER_NAME;
        interactable = this.GetComponent<Interactable>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        transitionManager = SingletonManager.Get<TransitionManager>();
        if (SingletonManager.Get<PlayerData>())
        {
            hasCompleted = SingletonManager.Get<PlayerData>().isGetWaterFinished;
        }
        Events.OnSceneChange.AddListener(OnSceneChange);
        StopInteractRoutine();
    }

    public override void Interact(GameObject player = null)
    {
        if (isInteracted) { return; }
        isInteracted = true;
        MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            //Check if has enough motivation
            if (playerMotivation.MotivationAmount < motivationCost)
            {
                // if there is not enough motivation amount
                Debug.Log("Not enough motivation");
                ShakeScreen();
                return;
            }
            else
            {
               // playerMotivation.DecreaseMotivation(motivationCost);
                //Disable player controls 
                PlayerControls playerControl = player.GetComponent<PlayerControls>();
                if (playerControl)
                {
                    playerControl.enabled = false;
                }
                Debug.Log("Interacted");
                isInteracted = true; // to avoid being called again since it is already interacted
                StartInteractRoutine();
            }
        }
        Debug.Log("Interacted");
    }

    public override void EndInteract(GameObject player = null)
    {
        base.EndInteract(player);
    }

    public override void JumpToMiniGame()
    {
        //base.JumpToMiniGame();
        if (sceneChange)
        {
            if (minigameScene != null)
            {
                //Remove all listeners because when the scene changes it will destroy the scene but will try to access the old active scripts
                Events.OnSceneChange.Invoke();
                Events.OnInteract.RemoveListener(Interact);
                Events.OnFinishInteract.RemoveListener(EndInteract);

                sceneChange.OnChangeScene(minigameScene);
            }
            else
            {
                Debug.Log("No next scene name");
            }
        }
        else
        {
            Debug.Log("No Scene change");
        }
    }

    //Can be improved and transfer to player controls 
    public override IEnumerator InteractCoroutine(GameObject player = null)
    {
        transitionManager = SingletonManager.Get<TransitionManager>();
        //Disable UI Elements
        SingletonManager.Get<UIManager>().DeactivateGameUI();

        //Play animation of transition
        if (transitionManager)
        {

            transitionManager.ChangeAnimation(TransitionManager.CURTAIN_CLOSE);

        }
        //Wait for the transition to end
        while (!transitionManager.IsAnimationFinished())
        {
            //Debug.Log("Closing Curtain Time: " + transitionManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }

        //Jump to next scene
        JumpToMiniGame();
        yield return null;
    }
    public override void StartInteractRoutine()
    {
        interactRoutine = StartCoroutine(InteractCoroutine());
    }
    public override void StopInteractRoutine()
    {
        if (interactRoutine != null)
        {
            StopCoroutine(interactRoutine);
            interactRoutine = null;
        }
    }
    public override void OnSceneChange()
    {
        Events.OnSceneChange.RemoveListener(OnSceneChange);
        Events.OnInteract.RemoveListener(Interact);
        Events.OnFinishInteract.RemoveListener(EndInteract);
        StopInteractRoutine();
        Debug.Log("Removed listener from Minigame");
    }
    private void OnDestroy()
    {
        //Used when switching scene since all will be destroyed. In case the invoke doesnt go through 
        StopAllCoroutines(); //Stop all coroutines and set it to null for the next playthrough to access the object reference
        OnSceneChange();
    }
}
