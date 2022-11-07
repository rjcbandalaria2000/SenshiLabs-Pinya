using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoManager : MonoBehaviour
{
    // Start is called before the first frame update

    VideoPlayer videoPlayer;
    public List<VideoClip> clips;
    public int counter;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        videoPlayer.clip = clips[0];
    }

    public void MoveVideo(int index)
    {
        
        videoPlayer.clip = clips[index];
    }

    public void NextVideo()
    {
        if (counter <= clips.Count - 1)
        {
            counter++;
            videoPlayer.clip = clips[counter];
        }
      
    }

    public void PrevVideo()
    {
        if(counter > 0)
        {
            counter--;

        }
        else
        {
            counter = 0;
        }

        videoPlayer.clip = clips[counter];
    }
}
