using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DebuggerManager : MonoBehaviour
{
    public GameObject       Player;

    private MotivationMeter playerMotivation;

    [Header("Change Values")]
    public float MotivationValueChange = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitializeDebugger();
    }

    public void InitializeDebugger()
    {
        Assert.IsNotNull(Player, "Player is null or is not set");
        playerMotivation = Player.GetComponent<MotivationMeter>();
    }

    public void OnIncreaseMotivationButtonClicked()
    {
        Assert.IsNotNull(playerMotivation, "PlayerMotivation not set or is null");
        playerMotivation.IncreaseMotivation(MotivationValueChange);
    }
    
    public void OnDecreaseMotivationButtonClicked()
    {
        Assert.IsNotNull(playerMotivation, "PlayerMotivation not set or is null");
        playerMotivation.DecreaseMotivation(MotivationValueChange);
    }

}
