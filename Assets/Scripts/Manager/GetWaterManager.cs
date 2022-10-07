using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWaterManager : MinigameManager
{
    [Header("Water UI")]
    public Slider       slider;

    [Header("Setup Values")]
    public int          RequiredNumSwipes = 3;
    public int          NumOfSwipes = 0;

    private void Awake()
    {
        //Trying this solution since
        //every destroy or load the singleton must be destroyed
        //and replaced with a new singeleton script when it exists
        GetWaterManager getWaterManager = SingletonManager.Get<GetWaterManager>();
        if (getWaterManager != null) {

            SingletonManager.Remove<GetWaterManager>();
            SingletonManager.Register(this);
        }
        else
        {
            SingletonManager.Register(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();

       
    }

    public override void Initialize()
    {
        if(sceneChange == null)
        {
            sceneChange = this.GetComponent<SceneChange>();
        }
        Events.OnObjectiveUpdate.Invoke();
    }

    public void CheckIfComplete()
    {
        if(NumOfSwipes == RequiredNumSwipes)
        {
            Debug.Log("Congratulations! You managed to get water");
            SingletonManager.Get<PlayerData>().IsGetWaterFinished = true;
        }
        else if(NumOfSwipes < RequiredNumSwipes)
        {
            Debug.Log("Try again next time");
        }
        else if(NumOfSwipes > RequiredNumSwipes)
        {
            Debug.Log("Whoops you broke the rope, try again");
        }
        if (sceneChange)
        {
            Events.OnSceneChange.Invoke();
            sceneChange.OnChangeScene(NameOfNextScene);
        }

    }

    public void SetNumOfSwipes(int count)
    {
        NumOfSwipes = count;
    }
   
}
