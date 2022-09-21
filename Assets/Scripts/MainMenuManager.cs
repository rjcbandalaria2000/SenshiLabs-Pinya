using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public SceneChange sceneChange;

    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<SceneChange>() != null)
        {
            sceneChange = this.GetComponent<SceneChange>();
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToMiniGameScene()
    {
        sceneChange.OnChangeScene(sceneName);
    }
}
