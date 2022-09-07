using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private float progressbarValue;
    public Slider loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        
        if(SingletonManager.Get<SceneLoad>() != null)
        {
            progressbarValue = SingletonManager.Get<SceneLoad>().getLoadProgress();
        }

        StartCoroutine(updatebar());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator updatebar()
    {
        while(progressbarValue >= 1)
        {
            if (SingletonManager.Get<SceneLoad>() != null)
            {
                progressbarValue = SingletonManager.Get<SceneLoad>().getLoadProgress();
            }
            loadingBar.value = progressbarValue;
        }
       
        yield return null;
    }
}
