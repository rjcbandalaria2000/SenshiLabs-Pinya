using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("DayCycle Timer")]
    public float timer;
    public float endTime;

    [Header("Mini-Game Timer")]
    public float currentTime;
    public float maxTime;
    public float speedCounter;

    public Player player;

    private void Awake()
    {
        SingletonManager.Register(this);

        
        currentTime = maxTime;
        initialize_MinigameTimer();// remove this line, for testing purpose only;
    }
    public void Start()
    {
        if(player == null)
        {
            player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
        }

        StartCoroutine(dayStart());
    }
    public IEnumerator dayStart()
    {
        if(SingletonManager.Get<DayCycle>() != null)
        {
            SingletonManager.Get<DayCycle>().time = timer;
            SingletonManager.Get<DayCycle>().endTime = endTime;
        }
        else
        {
            Debug.Log("DayCycle doesnt exist");
        }
        if(SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().activateTimerUI(); 
            //SingletonManager.Get<UIManager>().motivationMeter.SetActive(true); 
            //SingletonManager.Get<UIManager>().piñyaMeter.SetActive(true);
        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }

    public IEnumerator dayEnd()
    {
        if (SingletonManager.Get<UIManager>() != null)
        {
            SingletonManager.Get<UIManager>().deactivateTimerUI();
            SingletonManager.Get<UIManager>().activateDayEnd_UI();

        }
        else
        {
            Debug.Log("UI Manager doesnt exist");
        }

        yield return new WaitForSeconds(3f);
    }

    public void initialize_MinigameTimer() // call this when minigame starts
    {
        if (SingletonManager.Get<MiniGameTimer>() != null)
        {
            SingletonManager.Get<MiniGameTimer>().timer = currentTime;
            SingletonManager.Get<MiniGameTimer>().maxTimer = maxTime;
        }
    }

    public IEnumerator countdownTimer()
    {
        while(SingletonManager.Get<MiniGameTimer>().timer > 0)
        {
            SingletonManager.Get<MiniGameTimer>().timer--;
            SingletonManager.Get<DisplayMiniGameTimer>().updateMiniGameTimer();
            yield return new WaitForSeconds(speedCounter);
        }
    }
}
