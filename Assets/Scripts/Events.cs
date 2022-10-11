using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This is the Events.cs where all events that we will hold all the game events. 
  reference: https://www.youtube.com/watch?v=RPhTEJw6KbI&list=PLuiBbLS_hU1uu5bMXVceRpHBxO7fSmeLd&index=48
  please look at the Evt.cs to see and to further study on how the whole algorithm works. 
 */
public static class Events
{
    // To create an event with no parameters, please follow this format. 
    #region How to Use Events as static class 
    //    public static readonly Evt OnUnitDied = new Evt();

    //  //  public static readonly Evt<Unit> OnUnitSelect = new Evt();

    //    public static readonly Evt OnTowerDied = new Evt();

    //    public static readonly Evt OnTowerSelect = new Evt();

    //    public static readonly Evt OnResetInfoUI = new Evt();

    //    public static readonly Evt OnPlayerSelect = new Evt();

    //    public static readonly Evt OnNexusDestroy = new Evt();

    //    //public static readonly Evt<Unit,float> OnMiniUIUpdate = new Evt<Unit, float>();

    ////    public static readonly Evt<Unit> OnGameOver = new Evt<Unit>();

    //  //  public static readonly Evt<int> OnPlayerSkillIndex = new Evt<int>();

    //    //To create an event with parameters, please follow this format. 
    //    //public static readonly Evt<int> OnTakeDamage = new Evt<int>();

    // To use, call Events.(EventName)
    #endregion

    #region UI Events
    //Used for Meter UI Events when updating the values 
    public static readonly Evt OnChangeMeter = new();
    public static readonly Evt OnDisplayMinigameTime = new Evt();
    public static readonly Evt OnDisplayCycleTime = new Evt();
    #endregion

    #region Player Events

    #endregion

    #region Interactable Events
    public static readonly Evt<GameObject> OnInteract = new();
    public static readonly Evt<GameObject> OnFinishInteract = new();
    public static readonly Evt OnEnterInteraction = new();
    #endregion

    #region MouseEvents
    public static readonly Evt OnMouseHover = new();
    public static readonly Evt OnMouseDown = new();
    public static readonly Evt OnMouseUp = new();
    #endregion

    #region Minigame Events
    public static readonly Evt OnObjectiveUpdate = new();
    public static readonly Evt OnObjectiveComplete = new();
    public static readonly Evt OnGameStart = new();
    public static readonly Evt UpdateScore = new Evt();
    #region CleanTheHouseMinigame Events
    public static readonly Evt<GameObject> OnInteractCleanHouse = new();
    #endregion

    #region WashTheDishes Events

    public static readonly Evt OnPlateCleaned = new();

    #endregion

    #region GetWater Events

    public static readonly Evt OnBucketDown = new();
    public static readonly Evt OnWaterFilling = new();
    public static readonly Evt OnBucketUsed = new();
    public static readonly Evt OnBucketDrop = new(); // controls the arrows UI in Get Water Minigame (activates down arrow)
    public static readonly Evt OnBucketRetrieve = new(); //controls the arrows UI in Get Water Minigame (activates up arrow)
    #endregion


    #endregion

    #region SceneChange Events
    public static readonly Evt OnSceneChange = new();
    public static readonly Evt OnSceneLoad = new();
    #endregion

    #region TransitionEvents
    public static readonly Evt OnCurtainStart = new();
    public static readonly Evt OnCurtainsOpened = new();
    public static readonly Evt OnCurtainsClosed = new();

    #endregion

    #region PinyaMeter Events
    public static readonly Evt OnPinyaEmpty = new();
    #endregion
}
