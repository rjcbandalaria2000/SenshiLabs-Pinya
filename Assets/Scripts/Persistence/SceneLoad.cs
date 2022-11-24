using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class SceneLoad : MonoBehaviour
{
    [Header("Config")]
    public string FirstSceneId;

    private string currentSceneId;

    private float loadProgress;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();   
    private void Awake()
    {
        SingletonManager.Register(this);
    }
  
    private void Start()
    {
       // LoadScene(FirstSceneId);

        SceneManager.LoadSceneAsync(FirstSceneId,LoadSceneMode.Additive);
        currentSceneId = FirstSceneId;

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

      
    }

    private IEnumerator LoadGame(string sceneId)
    {

        yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
        //scenesLoading.Add(SceneManager.UnloadSceneAsync(currentSceneId));
        SceneManager.UnloadSceneAsync(currentSceneId);
        currentSceneId = string.Empty;

        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
       // scenesLoading.Add(operation);
        yield return operation;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneId));
        currentSceneId = sceneId;

        yield return null;

       // StartCoroutine(ProgressLoad());
    }

    //IEnumerator ProgressLoad()
    //{
    //    for(int i = 0; i < scenesLoading.Count; i++)
    //    {
    //        while(!scenesLoading[i].isDone)
    //        {
    //            yield return null;
    //        }


    //    }

    //    SingletonManager.Get<UIManager>().DeactivateLoadingUI();
    //}

    public Coroutine LoadScene(string sceneId)
    {
       // return StartCoroutine(LoadSequence(sceneId));

        return StartCoroutine(LoadGame(sceneId));
    }

}
