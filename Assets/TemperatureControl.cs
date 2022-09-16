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
    public GameObject TempPositions;
   
    public Vector2 RandomTempPosition;

    [Header("Temperature")]
    public GameObject ChosenTemp;
    public GameObject Parent;

    [Header("State")]
    public bool CanMove;

    private Vector3 destination;
    private Coroutine moveTrackerRoutine;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(Tracker, "Tracker is null or is not set");
        //SetRandomTemperaturePosition();

    }

    public void SetRandomTemperaturePosition()
    {
        TempPositions.transform.position = new Vector2(Random.Range(RandomTempPosition.x, RandomTempPosition.y), 
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
                Speed * Time.deltaTime);
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
        if(moveTrackerRoutine == null) { return; }
        StopCoroutine(moveTrackerRoutine);
        SetCookingTemp();
    }

}
