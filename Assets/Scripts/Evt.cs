using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*
  This is the Evt.cs where all abstract classes and functionality of the Event bus takes place.. 
  reference: https://www.youtube.com/watch?v=RPhTEJw6KbI&list=PLuiBbLS_hU1uu5bMXVceRpHBxO7fSmeLd&index=48
  please refer to the UnitSelection.cs to see the implementation. 
 */
public class Evt
{

 /*
  This class tells us how to create an event with no parameters
 */
    private event Action action = delegate { };

    public void Invoke()
    {
        action.Invoke();
    }

    public void AddListener(Action listener)
    {
       // action -= listener;
        action += listener;
    }

    public void RemoveListener(Action listener)
    {
        action -= listener;
    }

}
public class Evt<T>
{
    /*
  This class tells us how to create an event with parameters
 */
    private event Action<T> action = delegate { };

    public void Invoke(T param) { action.Invoke(param); }

    public void AddListener(Action<T> listener) { action += listener; }

    public void RemoveListener(Action<T> listener) { action -= listener; }
}