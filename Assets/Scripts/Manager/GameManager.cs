using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
   

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
    

    [Header("List of Task")]
    public List<string>             minigamesName;

    [Header("Pre-requisites")]
    public bool                     isGetWaterFinish;
    public bool                     isGroceryTaskFinish;
    
    private UIManager               UI;
    private TransitionManager       transitionManager;
    private Coroutine               startGameRoutine;

    private void Awake()
    {
        SingletonManager.Register(this);

        
        currentTime = maxTime;
        
    }
    public void Start()
    {
        isGetWaterFinish = SingletonManager.Get<PlayerData>().IsGetWaterFinished;
        isGroceryTaskFinish = SingletonManager.Get<PlayerData>().IsGroceryFinished;


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

        if(transitionManager == null)
        {
            transitionManager = SingletonManager.Get<TransitionManager>();
        }
        StartGameTransition();
    }

    public void StartGameTransition()
    {
        // for curtain transition
        startGameRoutine = StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        //Disable UI 
        UI.DeactivateGameUI();
        // Start playing curtain animation 
        transitionManager.ChangeAnimation(TransitionManager.CURTAIN_OPEN);

        //Wait for the animation to finish 
        while (!transitionManager.IsAnimationFinished()) {
            yield return null;
        }

        //Display UI
        UI.ActivateGameUI();
        yield return null;
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