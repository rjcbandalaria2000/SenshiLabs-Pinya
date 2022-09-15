using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoldingMinigameManager : MinigameManager
{
    public GameManager manager;
    public MiniGameTimer gameTimer;
    public Clothes ClothesComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.GetComponent<SceneChange>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CheckIfFinished()
    {
        if (ClothesComponent.clothes == 0)
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
}
