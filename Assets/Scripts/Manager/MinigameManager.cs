using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{

    [Header("Scene Change")]
    public string           NameOfNextScene;

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

    public virtual void OnWin()
    {

    }

    public virtual void OnLose()
    {

    }
    
}
