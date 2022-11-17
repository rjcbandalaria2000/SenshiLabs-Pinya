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
    private void Awake()
    {
        
    }
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NextState();
        }
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

    public void MorningState()
    {
        Debug.Log("Morning");
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(daySun.DOJumpAnchorPos(stateEndPos[(int)dayState], 200, 4, 1f, false)).WaitForCompletion();
        mySequence.Append(dayCloud.DOMoveX(1000f, 1, false)).WaitForCompletion();
        audioSource.PlayOneShot(soundSFX[(int)dayState]);
        WoodSound.Invoke();
        dayState++;
    }

    public void AfternoonState()
    {
        Debug.Log("Afternoon");
        int index = (int)dayState - 1;
        Sequence mySequence = DOTween.Sequence();
        statesGO[index].DOMoveX(-3000f, 1, false);
        skyBG.DOAnchorPosX(xSkyPos[index], 1, false);
        mySequence.Append(afternoonSun.DOJumpAnchorPos(stateEndPos[(int)dayState], 200, 4, 1f, false));
        mySequence.Append(afternoonCloud.DOAnchorPosX(-38f, 1, false));
        audioSource.PlayOneShot(soundSFX[(int)dayState]);
        WoodSound.Invoke();
        dayState++;
    }

    public void EveningState()
    {
        Debug.Log("Evening");
        int index = (int)dayState - 1;
        Sequence mySequence = DOTween.Sequence();
        statesGO[index].DOMoveX(-3000f, 1, false);
        skyBG.DOAnchorPosX(xSkyPos[index], 1, false);
        audioSource.PlayOneShot(soundSFX[(int)dayState]);
        WoodSound.Invoke();
        mySequence.Append(eveSun.DOJumpAnchorPos(stateEndPos[(int)dayState], 200, 4, 1f, false));
    }
}
