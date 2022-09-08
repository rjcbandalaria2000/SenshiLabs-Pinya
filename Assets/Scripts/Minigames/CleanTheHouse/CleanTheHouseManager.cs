using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CleanTheHouseManager : MonoBehaviour
{
    [Header("Setup Values")]
    public int  NumberOfTrash = 1;
    public int  NumberOfDust = 0;

    [Header("Player Values")]
    public int  NumberOfTrashThrown = 0;
    public int  NumberOfDustSwept = 0;

    [Header("Scene Change")]
    public string NameOfScene;
    private SceneChange sceneChange;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
        sceneChange = this.gameObject.GetComponent<SceneChange>();
    }

    public void CheckIfFinished()
    {
        if(NumberOfTrashThrown >= NumberOfTrash && NumberOfDustSwept >= NumberOfDust)
        {
            Debug.Log("Minigame complete");
            Assert.IsNotNull(sceneChange, "Scene change is null or not set");
            sceneChange.OnChangeScene(NameOfScene);
        }
    }

    public void AddTrashThrown(int count)
    {
        NumberOfTrashThrown += count;
        CheckIfFinished();
    }

    public void AddDustSwept(int count)
    {
        NumberOfDustSwept += count;
        CheckIfFinished();
    }

}
