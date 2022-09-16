using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private UIManager UI;

    [Header ("Meaters")]
    public MotivationMeter playerMotivation;
    public PinyaMeter playerPinyaMeter;

    [Header("Change Values")]
    public float MotivationValueChange;
    public float PinyaMeterValueChange;

    [Header("Ask Mom")]
    public List<GameObject> highlightObj;
    public float cooldown;

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
    public TagMiniGameManager tagMiniGame;
    public FoldingMinigameManager foldMiniGame; 

    private void Awake()
    {
        SingletonManager.Register(this);

        
        currentTime = maxTime;
        
    }
    public void Start()
    {
        if(UI == null)
        {
            if(GameObject.FindObjectOfType<UIManager>() != null)
            {
                UI = GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>();
            }
        }
        else
        {
            Debug.Log("Null UI");
        }
        

        if(highlightObj.Count > 0)
        {
            for(int i = 0; i < highlightObj.Count; i++)
            {
                highlightObj[i].SetActive(false);
            }
        }

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
        if(tagMiniGame == null)
        {
            if(GameObject.FindObjectOfType<TagMiniGameManager>() != null)
            {
                tagMiniGame = GameObject.FindObjectOfType<TagMiniGameManager>().GetComponent<TagMiniGameManager>();
            }
        }
        if(foldMiniGame == null)
        {
            if(GameObject.FindObjectsOfType<FoldingMinigameManager>() != null)
            {
                foldMiniGame = GameObject.FindObjectOfType<FoldingMinigameManager>().GetComponent<FoldingMinigameManager>();
            }
        }
     
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
            //SingletonManager.Get<UIManager>().pi�yaMeter.SetActive(true);
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

    public void AskMom()
    {
        if(playerPinyaMeter != null)
        {
            Debug.Log("Highlight Obj");
            StartCoroutine(startCD());
            Assert.IsNotNull(playerPinyaMeter, "PlayerPinyaMeter is null or is not set");
            playerPinyaMeter.DecreasePinyaMeter(PinyaMeterValueChange);
        }
    }

    IEnumerator startCD()
    {
        UI.buttonUninteractable();
        outLineObj();
        yield return new WaitForSeconds(cooldown);
        UI.buttonInteractable();
        unOutlineObj();
    }

    public void outLineObj()
    {
        if (highlightObj.Count > 0)
        {
            for (int i = 0; i < highlightObj.Count; i++)
            {
                highlightObj[i].SetActive(true);
            }
        }
    }

    public void unOutlineObj()
    {
        if (highlightObj.Count > 0)
        {
            for (int i = 0; i < highlightObj.Count; i++)
            {
                highlightObj[i].SetActive(false);
            }
        }
    }



   


}

//public void OnIncreaseMotivationButtonClicked()
//{
//    Assert.IsNotNull(playerMotivation, "PlayerMotivation not set or is null");
//    playerMotivation.IncreaseMotivation(MotivationValueChange);
//}

//public void OnDecreaseMotivationButtonClicked()
//{
//    Assert.IsNotNull(playerMotivation, "PlayerMotivation not set or is null");
//    playerMotivation.DecreaseMotivation(MotivationValueChange);
//}

//public void OnIncreasePinyaMeterButtonClicked()
//{
//    Assert.IsNotNull(playerPinyaMeter, "PlayerPinyaMeter is null or is not set");
//    playerPinyaMeter.IncreasePinyaMeter(PinyaMeterValueChange);
//}

//public void OnDecreasePinyaMeterButtonClicked()
//{
//    Assert.IsNotNull(playerPinyaMeter, "PlayerPinyaMeter is null or is not set");
//    playerPinyaMeter.DecreasePinyaMeter(PinyaMeterValueChange);
//}