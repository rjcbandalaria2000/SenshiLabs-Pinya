using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagMiniGame : MinigameObject
{
    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        interactable = this.GetComponent<Interactable>();

        if (interactable)
        {
            //Listen to the interactable event and proceed to interact with the player
            //Events.OnInteract.AddListener(Interact);
            //Events.OnFinishInteract.AddListener(EndInteract);

        }
        sceneChange = this.gameObject.GetComponent<SceneChange>();
    }

    public override void Interact(GameObject player = null)
    {
        Debug.Log("Interact with" + this.gameObject.name);
        MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            playerMotivation.DecreaseMotivation(motivationCost);
        }
        Debug.Log("Interacted");
        isInteracted = false;
        JumpToMiniGame();
        //isInteracted = true;
        //interactRoutine = StartCoroutine(InteractCoroutine(player));
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

    public override IEnumerator InteractCoroutine(GameObject player = null)
    {
        while (isInteracted)
        {
            Debug.Log("Interact with" + this.gameObject.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                MotivationMeter playerMotivation = player.GetComponent<MotivationMeter>();
                if (playerMotivation)
                {
                    playerMotivation.DecreaseMotivation(motivationCost);
                }
                Debug.Log("Interacted");
                isInteracted = false;
                yield return new WaitForSeconds(2.0f);
                JumpToMiniGame();
            }
            yield return null;
        }
    }

}