using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureControl : MonoBehaviour
{
    [Header("Tracker")]
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
        StartMoveTracker();
    }

    public void StartMoveTracker()
    {
        moveTrackerRoutine = StartCoroutine(MoveTracker());
    }

    IEnumerator MoveTracker()
    {
        while(Vector2.Distance(this.transform.position, EndPosition.transform.position) > 0)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, 
                EndPosition.transform.position,
                Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Temperature collidedTemp = collision.gameObject.GetComponent<Temperature>();
        if (collidedTemp)
        {
            ChosenTemp = collidedTemp.gameObject;
            Debug.Log("Collided temp");
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
