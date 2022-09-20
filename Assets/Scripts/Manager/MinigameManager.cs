using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{

    [Header("Scene Change")]
    public string           NameOfNextScene;

    [Header("Timers")]
    public float timer;
    public float maxTimer;

    public float speedTimer;

    protected SceneChange   sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Initialize()
    {

    }

    public virtual void CheckIfFinished()
    {

    }

    public virtual void OnMinigameFinished()
    {

    }

    public virtual void OnMinigameLose()
    {

    }


    public virtual void OnWin()
    {

    }

    public virtual void OnLose()
    {

    }
    
}
