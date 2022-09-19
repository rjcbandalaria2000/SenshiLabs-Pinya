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

    private float loadProgress;

    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    private void Start()
    {
        LoadScene(FirstSceneId);

        PlayerData playerData = SingletonManager.Get<PlayerData>();
    }

    public float getLoadProgress()
    {
        return loadProgress;
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

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        
        yield return operation;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneId));
        currentSceneId = sceneId;

        yield return null;

        if(SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().activateLoading_UI();
        }
        else { Debug.Log("NO LOADING SCREEN"); }
        

        while (!operation.isDone)
        {
            Debug.Log("Loading");
            loadProgress = Mathf.Clamp01(operation.progress/.9f);
            yield return null;
        }

        if (SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().deactivateLoading_UI();
        }
            

    }

    public Coroutine LoadScene(string sceneId)
    {
        return StartCoroutine(LoadSequence(sceneId));
    }

}
