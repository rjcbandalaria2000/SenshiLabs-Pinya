using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TemperatureControl : MonoBehaviour
{
    [Header("Tracker")]
    public GameObject Tracker;
    public float Speed; 
    [Header("Positions")]
    public GameObject StartPosition;
    public GameObject EndPosition;

    [Header("Temperature")]
    public GameObject ChosenTemp;
    public GameObject Parent;

    [Header("State")]
    public bool CanMove;

    private Coroutine moveTrackerRoutine;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Tracker, "Tracker is null or is not set");
        //StartMoveTracker();

    }

    public void StartMoveTracker()
    {
        moveTrackerRoutine = StartCoroutine(MoveTracker());
    }

    IEnumerator MoveTracker()
    {
        while(Vector2.Distance(this.transform.position, EndPosition.transform.position) > 0)
        {
            Tracker.transform.position = Vector2.MoveTowards(Tracker.transform.position, 
                EndPosition.transform.position,
                Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetCookingTemp()
    {
        if(ChosenTemp == null) { return; }
        Pot potParent = Parent.GetComponent<Pot>();
        if(potParent == null) { return; }
        potParent.CookingSpeed = ChosenTemp.GetComponent<Temperature>().CookingSpeed;
    }



    public void StopMoveTracker()
    {
        if(moveTrackerRoutine == null) { return; }
        StopCoroutine(moveTrackerRoutine);
        SetCookingTemp();
    }

}
