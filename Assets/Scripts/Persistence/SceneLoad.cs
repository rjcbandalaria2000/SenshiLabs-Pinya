using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Linq;
using System;

public class SceneLoad : MonoBehaviour
{
    [Header("Config")]
    public string FirstSceneId;

    private string currentSceneId;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    private void Start()
    {
        LoadScene(FirstSceneId);

        Player_Data playerData = SingletonManager.Get<Player_Data>();
    }

    private IEnumerator LoadSequence(string sceneId)
    {
        if (!string.IsNullOrEmpty(currentSceneId)) //currentsceneID != string.empty
        {
            SceneHandler addSceneLoader = SingletonManager.Get<SceneHandler>();

            if (addSceneLoader) //remove extra scenes to the current scene 
            {
                yield return addSceneLoader.UnloadScene();
                Debug.Log("Scenehandler_Unloaded");
            }
            

            yield return SceneManager.UnloadSceneAsync(currentSceneId);
            currentSceneId = string.Empty;
        }

        Resources.UnloadUnusedAssets();
        yield return null;
        GC.Collect(); // Trigger a collection to free memory
        yield return null;

        yield return SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        currentSceneId = sceneId;
    }

    public Coroutine LoadScene(string sceneId)
    {
        return StartCoroutine(LoadSequence(sceneId));
    }

}
