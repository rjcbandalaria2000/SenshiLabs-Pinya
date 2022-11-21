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

    public RectTransform skyBG;
    private DayState dayState;
    private TimePeriod currentTimePeriod;
    public RectTransform daySun;
    public RectTransform dayCloud;

    public RectTransform afternoonSun;
    public RectTransform afternoonCloud;

    public RectTransform eveSun;
    //public RectTransform eveCloud;

    public List<RectTransform> statesGO;

    public List<float> xSkyPos;

    public List<Vector3> stateEndPos;
    //  public List<Vector3> cloudEndPos;

    public List<AudioClip> soundSFX;
    AudioSource audioSource;

    public UnityEvent WoodSound;

    [Header("States")]
    public bool isFinished= false;

    private void Awake()
    {
        //SingletonManager.Register(this);
        audioSource = this.GetComponent<AudioSource>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    NextState();
        //}
    }

    public void NextState()
    {
        switch (dayState)
        {
            case DayState.Morning:
                MorningState();
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
                MorningState();
                break;
            case TimePeriod.Afternoon:
                AfternoonState();
                break;
            case TimePeriod.Evening:
                EveningState();
                break;
        }
    }

    public void MorningState()
    {
        isFinished = false;
        Debug.Log("Morning");
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(daySun.DOJumpAnchorPos(stateEndPos[(int)currentTimePeriod], 200, 4, 1f, false)).WaitForCompletion();
        mySequence.Append(dayCloud.DOMoveX(1000f, 1, false)).WaitForCompletion();
        audioSource.PlayOneShot(soundSFX[(int)currentTimePeriod]);
        WoodSound.Invoke();
        //dayState++;
        isFinished = true;
    }

    public void AfternoonState()
    {
        Debug.Log("Afternoon");
        int index = (int)currentTimePeriod - 1;
        Sequence mySequence = DOTween.Sequence();
        statesGO[index].DOMoveX(-3000f, 1, false);
        skyBG.DOAnchorPosX(xSkyPos[index], 1, false);
        mySequence.Append(afternoonSun.DOJumpAnchorPos(stateEndPos[(int)currentTimePeriod], 200, 4, 1f, false));
        mySequence.Append(afternoonCloud.DOAnchorPosX(-38f, 1, false));
        audioSource.PlayOneShot(soundSFX[(int)currentTimePeriod]);
        WoodSound.Invoke();
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
}
