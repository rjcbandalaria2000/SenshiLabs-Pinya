using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private UIManager               UI;

    [Header ("Meaters")]
    public MotivationMeter          playerMotivation;
    public PinyaMeter               playerPinyaMeter;

    [Header("Change Values")]
    public float                    MotivationValueChange;
    public float                    PinyaMeterValueChange;

    [Header("Mini-Game Timer")]
    public float                    currentTime;
    public float                    maxTime;
    public float                    speedCounter;

    public Player player;

    [Header("MiniGame Manager")]
    public List<MinigameObject>     minigames = new();
    public CleanTheHouseManager     cleanMiniGame; //Change Public MiniGame;
    public GroceryManager           groceryMiniGame;
    public HideSeekManager          hideseekMiniGame;
    public TagMiniGameManager       tagMiniGame;
    public FoldingMinigameManager   foldMiniGame; 

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
            if (GameObject.FindObjectOfType<FoldingMinigameManager>() != null)
            {
                foldMiniGame = GameObject.FindObjectOfType<FoldingMinigameManager>().GetComponent<FoldingMinigameManager>();
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