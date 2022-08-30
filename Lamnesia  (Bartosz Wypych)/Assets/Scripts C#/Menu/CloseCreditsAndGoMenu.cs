using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CloseCreditsAndGoMenu : MonoBehaviour
{
    PlayableDirector director;
    public GameObject credits;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            credits.SetActive(false);
            SceneManager.LoadScene(0);
        }
    }
}
