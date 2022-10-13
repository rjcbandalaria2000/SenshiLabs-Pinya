using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TemperatureControl : MonoBehaviour
{
    [Header("Tracker")]
    public GameObject   Tracker;
    public List<float>  Speeds = new(); 

    [Header("Positions")]
    public GameObject   StartPosition;
    public GameObject   EndPosition;
    public GameObject   TempPositions;
    public GameObject   leftRandomPosition;
    public GameObject   rightRandomPosition;

    [Header("Temperature")]
    public GameObject   ChosenTemp;
    public int          RequiredPointCounter = 3;
    public GameObject   Parent;

    [Header("State")]
    public bool         CanMove;

    private int         pointCounter = 0;
    private Vector3     destination;
    private Coroutine   moveTrackerRoutine;
    private Pot         pot;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Tracker, "Tracker is null or is not set");
        SetRandomTemperaturePosition();
        pot = Parent.GetComponent<Pot>();
        if(pot != null)
        {
            pot.ShowCookingStage(pointCounter);
        }
    }

    public void SetRandomTemperaturePosition()
    {
        if(leftRandomPosition == null) { return; }
        if(rightRandomPosition == null) { return; }
        TempPositions.transform.position = new Vector2(Random.Range(leftRandomPosition.transform.position.x, rightRandomPosition.transform.position.x), 
            TempPositions.transform.position.y);
    }

    public void StartMoveTracker()
    {
        //SetRandomTemperaturePosition();
        Tracker.transform.position = StartPosition.transform.position;
        destination = EndPosition.transform.position;
        moveTrackerRoutine = StartCoroutine(MoveTracker());
    }

    IEnumerator MoveTracker()
    {
        //while (Vector2.Distance(Tracker.transform.position, EndPosition.transform.position) > 0)
        //{
        //    Tracker.transform.position = Vector2.MoveTowards(Tracker.transform.position,
        //        EndPosition.transform.position,
        //        Speed * Time.deltaTime);
        //    yield return new WaitForFixedUpdate();
        //}

        while (true)
        {
            if(pointCounter > Speeds.Count) { yield return null; }
            if(Tracker.transform.position.x == EndPosition.transform.position.x)
            {
                destination = StartPosition.transform.position;
            }
            if(Tracker.transform.position.x == StartPosition.transform.position.x)
            {
                destination = EndPosition.transform.position;
            }
            Tracker.transform.position = Vector2.MoveTowards(Tracker.transform.position,
                destination,
                Speeds[pointCounter] * Time.deltaTime);
            yield return new WaitForFixedUpdate();
            //yield return null;
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
        if(ChosenTemp != null) {
            if (pointCounter < RequiredPointCounter)
            {
                pointCounter++;
                Tracker.transform.position = StartPosition.transform.position;
                SetRandomTemperaturePosition();
                Events.OnCookingButtonPressed.Invoke();
                
            }
            if (pointCounter >= RequiredPointCounter)
            {
                if (Parent.GetComponent<Pot>().IsCooked) { return; }
                if (moveTrackerRoutine == null) { return; }
                StopCoroutine(moveTrackerRoutine);
                Parent.GetComponent<Pot>().IsCooked = true;
                //Events.OnObjectiveComplete.Invoke();
            }
            if (pot != null)
            {
                pot.ShowCookingStage(pointCounter);
                Debug.Log("Update Cook Stage");
            }
        }
        else
        {
            Debug.Log("Wrong Timing");
        }

       
    }

    public int GetRemainingCookingCounter()
    {
        return RequiredPointCounter - pointCounter;
    }

}
