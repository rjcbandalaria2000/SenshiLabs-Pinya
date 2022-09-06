using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTheHouseManager : MonoBehaviour
{
    [Header("Setup Values")]
    public int  NumberOfTrash = 1;
    public int  NumberOfDust = 0;

    [Header("Player Values")]
    public int  NumberOfTrashThrown = 0;
    public int  NumberOfDustSwept = 0;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    private void Start()
    {
        Events.OnObjectiveUpdate.AddListener(CheckIfFinished);
    }

    public void CheckIfFinished()
    {
        if(NumberOfTrashThrown >= NumberOfTrash && NumberOfDustSwept >= NumberOfDust)
        {
            Debug.Log("Minigame complete");
        }
    }

    public void AddTrashThrown(int count)
    {
        NumberOfTrashThrown += count;
        CheckIfFinished();
    }

}
