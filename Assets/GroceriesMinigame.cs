using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceriesMinigame : MinigameObject
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Interact(GameObject player = null)
    {
        base.Interact(player);
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
        base.JumpToMiniGame();
    }

}
