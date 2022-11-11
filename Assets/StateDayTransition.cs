using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum DayState
{
    Morning,
    Afternoon,
    Evening
}
public class StateDayTransition : MonoBehaviour
{
    // Start is called before the first frame update

    public Image skyBG;
    public DayState dayState;
    public RectTransform daySun;
    public RectTransform dayCloud;

    public List<float> xSkyPos;

    public List<Vector3> stateEndPos;
    public List<Vector3> cloudEndPos;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(dayCloud.DOMoveX(1000f, 1, false)).WaitForCompletion();
        mySequence.Append(daySun.DOJumpAnchorPos(stateEndPos[(int)dayState], 200, 4, 1f, false)).WaitForCompletion();

        dayState++;
    }

    public void AfternoonState()
    {

    }

    public void EveningState()
    {

    }
}
