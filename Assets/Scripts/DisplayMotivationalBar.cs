using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class DisplayMotivationalBar : MonoBehaviour
{
    public GameObject Player;
    public Slider MotivationSlider;

    private MotivationMeter playerMotivation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeMotivationBar()
    {
        Assert.IsNotNull(Player, Player.name + "is not set or is null");
        playerMotivation = Player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            playerMotivation.EvtChangeMeter.AddListener(UpdateMotivationBar);
        }
    }

    public void UpdateMotivationBar()
    {
        Assert.IsNotNull(playerMotivation, "Player Motivation is not set or is null");
        MotivationSlider.value = playerMotivation.MotivationAmount;
        
    }

}
