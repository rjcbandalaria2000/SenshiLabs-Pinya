using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public List<string> AdditionalScene;


    private void Awake()
    {
        SingletonManager.Register(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneSequence());
    }

    private IEnumerator LoadSceneSequence()
    {
        for (int i = 0; i < AdditionalScene.Count; i++)
        {
            yield return SceneManager.LoadSceneAsync(AdditionalScene[i], LoadSceneMode.Additive);
        }

    }

    private IEnumerator UnloadSceneRoutine()
    {
        for (int i = 0; i < AdditionalScene.Count; i++)
        {
            yield return SceneManager.UnloadSceneAsync(AdditionalScene[i]);
        }
    }

    public Coroutine UnloadScene()
    {
        return StartCoroutine(UnloadSceneRoutine());
    }
}
