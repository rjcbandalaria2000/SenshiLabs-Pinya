using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TagMiniGame : MinigameManager
{
    public PlayerTag player;

    public GameManager manager;
    public MiniGameTimer minigameTime;
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.gameObject.GetComponent<SceneChange>();
        minigameTime = this.gameObject.GetComponent<MiniGameTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(minigameTime.getTimer() <= 0)
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
