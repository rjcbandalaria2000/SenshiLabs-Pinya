using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("State")]
    public bool CanMove;

    private Coroutine moveTrackerRoutine;
    // Start is called before the first frame update
    void Start()
    {
        StartMoveTracker();
    }

    public void StartMoveTracker()
    {
        moveTrackerRoutine = StartCoroutine(MoveTracker());
    }

    IEnumerator MoveTracker()
    {
        while(Vector2.Distance(Tracker.transform.position, EndPosition.transform.position) > 0)
        {
            Tracker.transform.position = Vector2.MoveTowards(Tracker.transform.position, 
                EndPosition.transform.position,
                Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();

        }
    }

}
