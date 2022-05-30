using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lamnesia.InGame.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField]public AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;
        public bool isSoundOn { get; private set; }
        public bool isMusicOn { get; private set; }

        void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void Start()
        {
            isSoundOn = PlayerPrefs.GetInt("CurrentStateSound") != 0;
            isMusicOn = PlayerPrefs.GetInt("CurrentStateMusic") != 0;
        }

        public void PlaySound(AudioClip clip)
        {
            soundSource.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip, bool loopState)
        {
            musicSource.PlayOneShot(clip);
            musicSource.loop = loopState;
        }

        public void PlayMusic(AudioClip clip)
        {
            musicSource.PlayOneShot(clip);
        }
        
        public void SetMusicState(bool turnOn)
        {
            if (turnOn)
            {
                musicSource.volume = 1;
                isMusicOn = true;
                PlayerPrefs.SetInt("CurrentStateMusic", 1);
            }
            else
            {
                musicSource.volume = 0;
                isMusicOn = false;
                PlayerPrefs.SetInt("CurrentStateMusic", 0);
            }
        }

        public void SetSoundState(bool turnOn)
        {
            if (turnOn)
            {
                soundSource.volume = 1;
                isSoundOn = true;
                PlayerPrefs.SetInt("CurrentStateSound", 1);
            }
            else
            {
                soundSource.volume = 0;
                isSoundOn = false;
                PlayerPrefs.SetInt("CurrentStateSound", 0);
            } 
        }
    }
}
