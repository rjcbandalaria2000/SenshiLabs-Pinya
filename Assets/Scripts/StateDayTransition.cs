using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public enum DayState
{
    Morning,
    Afternoon,
    Evening
}
public class StateDayTransition : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform        skyBG;
    private DayState            dayState;
    private TimePeriod          currentTimePeriod;
    public RectTransform        daySun;
    public RectTransform        dayCloud;

    public RectTransform        afternoonSun;
    public RectTransform        afternoonCloud;

    public RectTransform        eveSun;
    //public RectTransform eveCloud;

    public List<RectTransform>  statesGO;

    public List<float>          xSkyPos;

    public List<Vector3>        stateEndPos;
    //  public List<Vector3> cloudEndPos;

    public List<AudioClip>      soundSFX;
    AudioSource                 audioSource;

    public UnityEvent           WoodSound;

    [Header("States")]
    public bool                 isFinished = false;
    public bool                 isMorning = false;
    public bool                 isAfternoon = false;
    public bool                 isEvening = false;

    private Coroutine morningCoroutine;

    private void Awake()
    {
        //SingletonManager.Register(this);
        audioSource = this.GetComponent<AudioSource>();
        if (SingletonManager.Get<PlayerData>())
        {
            if (SingletonManager.Get<PlayerData>().hasSaved)
            {
                isMorning = SingletonManager.Get<PlayerData>().isMorning;
                isAfternoon = SingletonManager.Get<PlayerData>().isAfternoon;
                isEvening = SingletonManager.Get<PlayerData>().isEvening;
            }
        }
    }

    public void NextState()
    {
        switch (dayState)
        {
            case DayState.Morning:
                StartMorningState();
                break;
            case DayState.Afternoon:
                AfternoonState();
                break;
            case DayState.Evening:
                EveningState();
                break;
        }
    }

    public void NextState(TimePeriod timePeriod)
    {
        currentTimePeriod = timePeriod;
        switch (timePeriod)
        {
            case TimePeriod.Morning:
                StartMorningState();
                break;
            case TimePeriod.Afternoon:
                AfternoonState();
                break;
            case TimePeriod.Evening:
                EveningState();
                break;
        }
    }

    public void StartMorningState()
    {
        if(isMorning) {
            Debug.Log("Already played morning");
            this.gameObject.SetActive(false);
            isFinished = true;
            return; }
        if(morningCoroutine != null)
        {
            StopMorningState();
        }
        morningCoroutine = StartCoroutine(AnimateMorningState());
    }

    public void StopMorningState() {
        if (morningCoroutine != null)
        {
            StopCoroutine(morningCoroutine);
            morningCoroutine = null;
        }
    }

    IEnumerator AnimateMorningState()
    {
        this.gameObject.SetActive(true);
        isFinished = false;
        Debug.Log("Morning");
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(daySun.DOJumpAnchorPos(stateEndPos[(int)currentTimePeriod], 200, 4, 1f, false)).WaitForCompletion();
        mySequence.Append(dayCloud.DOMoveX(1000f, 1, false)).WaitForCompletion();
        
        audioSource.PlayOneShot(soundSFX[(int)currentTimePeriod]);
        WoodSound.Invoke();
        yield return mySequence.WaitForCompletion();
        
        yield return new WaitForSeconds(0.5f);
        isFinished = true;
        this.gameObject.SetActive(false);
        isMorning = true;
        yield return null;
    }

    public void AfternoonState()
    {
        
    }

    IEnumerator AnimateAfternoonState()
    {
        Debug.Log("Afternoon");
        int index = (int)currentTimePeriod - 1;
        Sequence mySequence = DOTween.Sequence();
        statesGO[index].DOMoveX(-3000f, 1, false);
        skyBG.DOAnchorPosX(xSkyPos[index], 1, false);
        mySequence.Append(afternoonSun.DOJumpAnchorPos(stateEndPos[(int)currentTimePeriod], 200, 4, 1f, false));
        mySequence.Append(afternoonCloud.DOAnchorPosX(-38f, 1, false));
        
        yield return mySequence.WaitForCompletion();
        audioSource.PlayOneShot(soundSFX[(int)currentTimePeriod]);
        WoodSound.Invoke();
        yield return null;
        //dayState++;
    }

    public void EveningState()
    {
        Debug.Log("Evening");
        int index = (int)currentTimePeriod - 1;
        Sequence mySequence = DOTween.Sequence();
        statesGO[index].DOMoveX(-3000f, 1, false);
        skyBG.DOAnchorPosX(xSkyPos[index], 1, false);
        audioSource.PlayOneShot(soundSFX[(int)currentTimePeriod]);
        WoodSound.Invoke();
        mySequence.Append(eveSun.DOJumpAnchorPos(stateEndPos[(int)currentTimePeriod], 200, 4, 1f, false));
    }

    public void OnSceneChange()
    {
        StopMorningState();
        SingletonManager.Get<PlayerData>().isMorning = isMorning;
        SingletonManager.Get<PlayerData>().isAfternoon = isAfternoon;
        SingletonManager.Get<PlayerData>().isEvening = isEvening;
    }

    private void OnDestroy()
    {
        OnSceneChange();
    }
}
