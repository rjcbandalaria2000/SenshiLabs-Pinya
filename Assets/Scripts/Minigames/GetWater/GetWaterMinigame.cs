using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWaterMinigame : MinigameObject
{
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        Interactable = this.GetComponent<Interactable>();
        sceneChange = this.gameObject.GetComponent<SceneChange>();
    }

    public override void Interact(GameObject player = null)
    {
        isInteracted = true;
        MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            playerMotivation.DecreaseMotivation(MotivationCost);
        }
        Debug.Log("Interacted");
        isInteracted = false;
        JumpToMiniGame();
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
            if (MinigameScene != null)
            {
                //Remove all listeners because when the scene changes it will destroy the scene but will try to access the old active scripts
                Events.OnSceneChange.Invoke();
                Events.OnInteract.RemoveListener(Interact);
                Events.OnFinishInteract.RemoveListener(EndInteract);

                sceneChange.OnChangeScene(MinigameScene);
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
        yield return null;
    }
}
