using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FoldingMinigameManager : MinigameManager
{

    public Clothes ClothesComponent;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneChange = this.GetComponent<SceneChange>();
     

        if (ClothesComponent == null)
        {
            if(GameObject.FindObjectOfType<Clothes>() != null)
            {
                ClothesComponent = GameObject.FindObjectOfType<Clothes>().GetComponent<Clothes>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SingletonManager.Get<MiniGameTimer>().GetTimer() <= 0)
        {
            CheckIfFinished();
        }
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
