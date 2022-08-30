using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    void Update()
    {
       if (!videoPlayer.isPlaying)
        {
            SceneManager.LoadScene(1);
        }
    }
}
