using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DisplayPinyaMeter : MonoBehaviour
{
    public GameObject   Player;
    public Slider       PinyaSlider;

    private PinyaMeter playerPinyaMeter;

    private void Awake()
    {
        SingletonManager.Register(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePinyaBar();
    }

    public void InitializePinyaBar()
    {
        if (PinyaSlider == null) {
            PinyaSlider = this.GetComponent<Slider>();
        }
        if (Player == null)
        {
            Player = SingletonManager.Get<GameManager>().player.gameObject;
        }
        Assert.IsNotNull(Player, "Player is null or is not set");
        playerPinyaMeter = Player.GetComponent<PinyaMeter>();
        if (playerPinyaMeter)
        {
            PinyaSlider.maxValue = playerPinyaMeter.MaxPinyaValue;
            PinyaSlider.value = playerPinyaMeter.PinyaValue;
            Events.OnChangeMeter.AddListener(UpdatePinyaBar);
            Events.OnSceneChange.AddListener(RemoveListeners);
            
        }
    }
    
    public void UpdatePinyaBar()
    {
        Assert.IsNotNull(playerPinyaMeter, "Player Pinya Meter is null or is not set");
        PinyaSlider.value = playerPinyaMeter.PinyaValue;
    }

    public void RemoveListeners()
    {
        Events.OnChangeMeter.RemoveListener(UpdatePinyaBar);
        Events.OnSceneChange.RemoveListener(RemoveListeners);
    }

}