using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("DayCycle Timer")]
    public float timer;
    public float endTime;

    [Header("Mini-Game Timer")]
    public float currentTime;
    public float maxTime;
    public float speedCounter;

    public Player player;

    [Header("MiniGame Manager")]    
    public CleanTheHouseManager cleanMiniGame; //Change Public MiniGame;
    public GroceryManager groceryMiniGame;
    public HideSeekManager hideseekMiniGame;

    private void Awake()
    {
        SingletonManager.Register(this);

        
        currentTime = maxTime;
        
    }
    public void Start()
    {
        if(player == null)
        {
            if(GameObject.FindObjectOfType<Player>() != null)
            {
                player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
            }
            
        }
        if(cleanMiniGame == null)
        {
            if(GameObject.FindObjectOfType<CleanTheHouseManager>() != null)
            {
                cleanMiniGame = GameObject.FindObjectOfType<CleanTheHouseManager>().GetComponent<CleanTheHouseManager>();
            }
        }
        if(groceryMiniGame == null)
        {
            if(GameObject.FindObjectOfType<GroceryManager>() != null)
            {
                groceryMiniGame = GameObject.FindObjectOfType<GroceryManager>().GetComponent<GroceryManager>();
            }
        }
        if (hideseekMiniGame == null)
        {
            if (GameObject.FindObjectOfType<HideSeekManager>() != null)
            {
                hideseekMiniGame = GameObject.FindObjectOfType<HideSeekManager>().GetComponent<HideSeekManager>();
            }
        }
        //if(miniGame!=null)
        //{
        //    if (GameObject.FindObjectOfType<MiniGame>() != null)
        //    {
        //        minigame = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        //    }
        //}

        StartCoroutine(dayStart());
    }
    public IEnumerator dayStart()
    {
        if(SingletonManager.Get<DayCycle>() != null)
        {
            SingletonManager.Get<DayCycle>().time = timer;
            SingletonManager.Get<DayCycle>().endTime = endTime;
        }
        else
        {
            Debug.Log("DayCycle doesnt exist");
        }
        if(SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().activateTimerUI(); 
            //SingletonManager.Get<UIManager>().motivationMeter.SetActive(true); 
            //SingletonManager.Get<UIManager>().piñyaMeter.SetActive(true);
        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator dayEnd()
    {
        if (SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().deactivateTimerUI();
            SingletonManager.Get<UIManager>().activateDayEnd_UI();

        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }

  
  
    
}
