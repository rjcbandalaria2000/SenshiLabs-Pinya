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
    public GameObject rawImage;

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
        if(index <= 0)
        {
            videoPlayer.clip = clips[0];
        }
        else
        {
            videoPlayer.clip = clips[index];

        }
    }

    public void NextVideo()
    {
        if (rawImage.activeInHierarchy)
        {
            videoPlayer.clip = clips[counter];

            if (counter < clips.Count - 1)
            {
                // videoPlayer.gameObject
            
                counter++;
            }
            else
            {
                counter = clips.Count - 1;
            }
        }
       

    }

    public void PrevVideo()
    {
  
        if (counter <= 0)
        {
            counter = 0;
        }
        else
        {
            counter--;
        }

        if (rawImage.activeInHierarchy)
        {
            videoPlayer.clip = clips[counter];
        }

    }
}
