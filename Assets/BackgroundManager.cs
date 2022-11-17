using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background")]
    public GameObject       backgroundGO;

    [Header("Time Period BG Sprites")]
    public Sprite           morningBGSprite;
    public Sprite           afternoonBGSprite;
    public Sprite           eveningBGSprite;

    [Header("Time Period")]
    public TimePeriod       currentTimePeriod;

    private SpriteRenderer  backgroundSpriteRenderer;
    private PlayerData      playerData;

    private void Awake()
    {
        if (backgroundGO)
        {
            backgroundSpriteRenderer = backgroundGO.GetComponent<SpriteRenderer>();
        }
        playerData = SingletonManager.Get<PlayerData>();
        if (playerData)
        {
            currentTimePeriod = playerData.savedTimePeriod;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SetBackgroundTimeOfDay(currentTimePeriod);
    }

    public void SetBackgroundTimeOfDay(TimePeriod timePeriod)
    {
        Assert.IsNotNull(backgroundGO, "Background is not set or null");
        Assert.IsNotNull(backgroundSpriteRenderer, "BG sprite renderer is not set or null");
        switch (timePeriod)
        {
            case TimePeriod.Morning:
                if (morningBGSprite == null) { break; }
                backgroundSpriteRenderer.sprite = morningBGSprite;
                break;

            case TimePeriod.Afternoon:
                if (afternoonBGSprite == null) { break; }
                backgroundSpriteRenderer.sprite = afternoonBGSprite;
                break;

            case TimePeriod.Evening:
                if (eveningBGSprite == null) { break; }
                backgroundSpriteRenderer.sprite = eveningBGSprite;
                break;

            default:
                backgroundSpriteRenderer.sprite = morningBGSprite;
                break;
        }
    }
    
}
