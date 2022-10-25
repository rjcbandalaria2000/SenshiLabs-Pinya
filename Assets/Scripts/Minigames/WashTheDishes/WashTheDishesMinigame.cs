using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashTheDishesMinigame : MinigameObject
{
    private TransitionManager transitionManager;

    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public override void Initialize()
    {
        interactable = this.GetComponent<Interactable>();
        sceneChange = this.GetComponent<SceneChange>();
        transitionManager = SingletonManager.Get<TransitionManager>();
        isInteracted = false;
        if (SingletonManager.Get<PlayerData>())
        {
            hasCompleted = SingletonManager.Get<PlayerData>().isWashTheDishesFinished;
        }
    }

    public override void Interact(GameObject player = null)
    {
        //if(!SingletonManager.Get<PlayerData>().IsGetWaterFinished)
        //{
        //    Debug.Log("Finish Pre-Req");
        //    return;
        //}
        isInteracted = true;
        MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            //Check if has enough motivation
            if (playerMotivation.MotivationAmount < motivationCost)
            {
                // if there is not enough motivation amount
                Debug.Log("Not enough motivation");
                return;
            }
            else
            {
                playerMotivation.DecreaseMotivation(motivationCost);
                //Disable player controls 
                PlayerControls playerControl = player.GetComponent<PlayerControls>();
                if (playerControl)
                {
                    playerControl.enabled = false;
                }
                Debug.Log("Interacted");
                isInteracted = true; // to avoid being called again since it is already interacted
                StartInteractRoutine();
                //JumpToMiniGame();
            }
        }
        Debug.Log("Interacted");
        //JumpToMiniGame();
    }

    public override void EndInteract(GameObject player = null)
    {
        base.EndInteract(player);
    }

    public override void StartInteractRoutine()
    {
        interactRoutine = StartCoroutine(InteractCoroutine());   
    }

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
            Debug.Log("Closing Curtain Time: " + transitionManager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }

        //Jump to next scene
        JumpToMiniGame();
        yield return null;
    }

    public override void JumpToMiniGame()
    {
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
}
