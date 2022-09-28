using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class DisplayPinyaMeter : MonoBehaviour
{
    public GameObject   Player;
    public Slider       PinyaSlider;

    [Header("Effect")]
    public Image        damageBarImage;
    public float        fadeTime = 1f;
    public float        fadeAmount = 1f;

    private float       fadeTimer;
    private Color       damageBarColor;
    private PinyaMeter  playerPinyaMeter;
    private Coroutine   damageFadeRoutine;

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
            PinyaSlider.maxValue = playerPinyaMeter.maxPinyaValue;
            PinyaSlider.value = playerPinyaMeter.pinyaValue;
            Events.OnChangeMeter.AddListener(UpdatePinyaBar);
            Events.OnSceneChange.AddListener(RemoveListeners);
        }
        if (damageBarImage)
        {
            
        }
    }
    
    public void UpdatePinyaBar()
    {
        Assert.IsNotNull(playerPinyaMeter, "Player Pinya Meter is null or is not set");
        PinyaSlider.value = playerPinyaMeter.pinyaValue;
    }

    public void RemoveListeners()
    {
        Events.OnChangeMeter.RemoveListener(UpdatePinyaBar);
        Events.OnSceneChange.RemoveListener(RemoveListeners);
    }

    public void StartDamageFade()
    {
        damageFadeRoutine = StartCoroutine(DamageFade());
    }

    IEnumerator DamageFade()
    {
        
        yield return null;  
    }
}
