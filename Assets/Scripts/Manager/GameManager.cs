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
    //[Header("MiniGame Manager")]    
    //public MinigameManager miniGames;

    ////public CleanTheHouseManager cleanMiniGame; //Change Public MiniGame;
    ////public GroceryManager groceryMiniGame;
    ////public HideSeekManager hideseekMiniGame;
    ////public TagMiniGameManager tagMiniGame;
    ////public FoldingMinigameManager foldMiniGame;

    [Header("Pre-requisites")]
    public bool isGetWaterFinish;
    public bool isGroceryTaskFinish;


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

        if (player == null)
        {
            if (GameObject.FindObjectOfType<Player>() != null)
            {
                player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
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