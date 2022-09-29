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
    public Image        damageBarImage; // the fader image 
    public float        maxFadeTime = 1f;
    public float        fadeAmount = 1f;

    private float       fadeTimer; // moving timer for the fade time
    private Color       damageBarColor; // get the color property of the damageBarImage
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
        // Set up DamageEffect Fade
        // Code Reference: https://www.youtube.com/watch?v=cR8jP8OGbhM
        if (damageBarImage)
        {
            damageBarColor = damageBarImage.color;
            damageBarColor.a = 0f;
            damageBarImage.color = damageBarColor;
            damageBarImage.fillAmount = PinyaSlider.value / PinyaSlider.maxValue;
        }
    }
    
    public void UpdatePinyaBar()
    {
        Assert.IsNotNull(playerPinyaMeter, "Player Pinya Meter is null or is not set");
        //StartDamageFade();
        PinyaSlider.value = playerPinyaMeter.pinyaValue;
        
    }

    public void RemoveListeners()
    {
        Events.OnChangeMeter.RemoveListener(UpdatePinyaBar);
        Events.OnSceneChange.RemoveListener(RemoveListeners);
    }

    public void StartDamageFade()
    {
        //Show the damage
        if (damageBarColor.a <= 0)
        {
            damageBarImage.fillAmount = (float)PinyaSlider.value / (float)PinyaSlider.maxValue;
            Debug.Log("Fill Amount Damage Fade: " + damageBarImage.fillAmount);
        }
        damageBarColor.a = 1f;
        damageBarImage.color = damageBarColor;
        fadeTimer = maxFadeTime;
        damageFadeRoutine = StartCoroutine(DamageFade());
        Debug.Log("Start fade");
    }

    IEnumerator DamageFade()
    {
        //check if the damage bar is visible
        if(damageBarColor.a > 0)
        {
           
            while (fadeTimer > 0)
            {
                //Start fading
                Debug.Log("Damage fading");

                damageBarColor.a -= fadeAmount * Time.deltaTime;
                damageBarImage.color = damageBarColor;
                fadeTimer -= 1* Time.deltaTime;
                yield return null;  

            }
        }
        //damageBarColor.a = 1f;
        
    }
}
