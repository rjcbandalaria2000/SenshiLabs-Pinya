using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMinigame : MonoBehaviour
{
    public PlayerAI AIPlayer;
    public int waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        AIPlayer = GameObject.FindObjectOfType<PlayerAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     void OnMouseDown()
    {
      // AIPlayer.goToTarget(waypointIndex);
       StartCoroutine(AIPlayer.goToTargetRoutine(waypointIndex));
        Debug.Log("Go To Target");
    }
}
