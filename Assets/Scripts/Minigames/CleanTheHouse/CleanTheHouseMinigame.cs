using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTheHouseMinigame : MinigameObject
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        interactable = this.GetComponent<Interactable>();
        hasCompleted = SingletonManager.Get<PlayerData>().IsCleanTheHouseFinished;
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        isInteracted = false;
    }

    public override void Interact(GameObject player = null)
    {
        if (!isInteracted)
        {
            Debug.Log("Interact with" + this.gameObject.name);
            MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
            if (playerMotivation)
            {
                playerMotivation.DecreaseMotivation(motivationCost);
            }
            Debug.Log("Interacted");
            isInteracted = true;
            JumpToMiniGame();
        }
    }

    public override void EndInteract(GameObject player = null)
    {
        base.EndInteract(player);
        //StopCoroutine(interactRoutine);
    }

    public override void JumpToMiniGame()
    {
        if (sceneChange)
        {
            if(minigameScene != null)
            {
                SingletonManager.Get<PlayerData>().MinigamesPlayed++;
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

    public override IEnumerator InteractCoroutine(GameObject player = null)
    {
       yield return null;
    }

}
