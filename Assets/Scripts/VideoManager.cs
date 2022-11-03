using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoManager : MonoBehaviour
{
    // Start is called before the first frame update

    VideoPlayer videoPlayer;
    public List<VideoClip> clips;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }


    public void MoveVideo(int index)
    {
        videoPlayer.clip = clips[index];
    }
}
