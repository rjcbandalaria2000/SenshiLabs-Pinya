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
    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeMotivationBar();
    }

    public void InitializeMotivationBar()
    { 
        if(MotivationSlider == null)
        {
            MotivationSlider = this.GetComponent<Slider>();
        }
        Assert.IsNotNull(Player, Player.name + "is not set or is null");
        playerMotivation = Player.GetComponent<MotivationMeter>();
        if (playerMotivation)
        {
            MotivationSlider.maxValue = playerMotivation.MaxMotivation; 
            MotivationSlider.value = playerMotivation.MotivationAmount;
            playerMotivation.EvtChangeMeter.AddListener(UpdateMotivationBar);
        }
       
    }

    public void UpdateMotivationBar()
    {
        Assert.IsNotNull(playerMotivation, "Player Motivation is not set or is null");
        MotivationSlider.value = playerMotivation.MotivationAmount;
    }

}
