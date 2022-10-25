using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceriesMinigame : MinigameObject
{

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
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        if (SingletonManager.Get<PlayerData>())
        {
            hasCompleted = SingletonManager.Get<PlayerData>().isGroceryFinished;
        }
    }

    public override void Interact(GameObject player = null)
    {
        isInteracted = true;
        MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            playerMotivation.DecreaseMotivation(motivationCost);
        }
        Debug.Log("Interacted");
        JumpToMiniGame();
    }

    public override void EndInteract(GameObject player = null)
    {
        base.EndInteract(player);
    }

    public override IEnumerator InteractCoroutine(GameObject player = null)
    {
        return base.InteractCoroutine(player);
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
