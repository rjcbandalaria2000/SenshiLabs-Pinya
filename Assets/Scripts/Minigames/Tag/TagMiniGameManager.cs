using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TagMiniGameManager : MinigameManager
{
    public PlayerTag player;


    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.gameObject.GetComponent<SceneChange>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().getTimer() <= 0)
        {
            CheckIfFinished();
        }
    }

    public override void CheckIfFinished()
    {
        if (player.isTag == false)
        {
            Debug.Log("Minigame complete");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfNextScene);
        }
        else 
        {
            Debug.Log("Minigame lose");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfNextScene);
        }

    }

    //public override void OnMinigameLose()
    //{
    //    Debug.Log("Minigame lose");
    //    Assert.IsNotNull(sceneChange, "Scene change is null or not set");
    //    sceneChange.OnChangeScene(NameOfNextScene);
    //}
}
