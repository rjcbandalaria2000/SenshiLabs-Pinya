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

        if (rawImage.activeInHierarchy)
        {
            if (index <= 0)
            {
                videoPlayer.clip = clips[0];
            }
            else
            {
                videoPlayer.clip = clips[index];

            }
        }
       
    }

    public void NextVideo()
    {
        if (rawImage.activeInHierarchy)
        {
            if (counter < clips.Count)
            {
                // videoPlayer.gameObject
                videoPlayer.clip = clips[counter];
                counter++;
            }
            else
            {
                counter = clips.Count - 1;
                videoPlayer.clip = clips[counter];
            }
        }
       

    }

    public void PrevVideo()
    {
       
        if (counter <= 0)
        {
            counter = 0;
              videoPlayer.clip = clips[counter];
        }
        else
        {
            // counter--;
            if (counter >= clips.Count)
            {
             
                counter -= 2;
                videoPlayer.clip = clips[counter];

            }
            else
            {
                counter--;
                videoPlayer.clip = clips[counter];
            }

        }

    

    }
}
