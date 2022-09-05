using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void OnChangeScene(string sceneID)
    {
        SingletonManager.Get<SceneLoad>().LoadScene(sceneID);
    }
}
