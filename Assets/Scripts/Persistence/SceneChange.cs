using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public void OnChangeScene(string sceneID)
    {
        //  SingletonManager.Get<Player_Data>().storeData(SingletonManager.Get<GameManager>().player);
        if (SingletonManager.Get<UIManager>() != null)
        {
            if (SingletonManager.Get<UIManager>().loadingUI != null)
            {
                SingletonManager.Get<UIManager>().ActivateLoadingUI();
            }
            
        }
        else
        {
            Debug.Log("NO LOADING SCREEN");
        }
        SingletonManager.Get<SceneLoad>().LoadScene(sceneID);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
