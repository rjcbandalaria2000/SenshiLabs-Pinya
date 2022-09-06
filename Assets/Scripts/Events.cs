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
    #region UI

    public static readonly Evt OnDisplayMinigameTime = new Evt();
    public static readonly Evt OnDisplayCycleTime = new Evt();

    #endregion


    //  public static readonly Evt<Unit> OnUnitSelect = new Evt();

    //public static readonly Evt OnTowerDied = new Evt();

    //public static readonly Evt OnTowerSelect = new Evt();

    //public static readonly Evt OnResetInfoUI = new Evt();

    //public static readonly Evt OnPlayerSelect = new Evt();

    //public static readonly Evt OnNexusDestroy = new Evt();

    //public static readonly Evt<Unit,float> OnMiniUIUpdate = new Evt<Unit, float>();

//    public static readonly Evt<Unit> OnGameOver = new Evt<Unit>();

  //  public static readonly Evt<int> OnPlayerSkillIndex = new Evt<int>();

    //To create an event with parameters, please follow this format. 
    //public static readonly Evt<int> OnTakeDamage = new Evt<int>();

}
