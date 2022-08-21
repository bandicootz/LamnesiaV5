using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Lamnesia.Player
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 3600;
        public bool timerIsRunning = false;
        TextMeshProUGUI timeText;
        HealthScript healthScript;

        private float time = 0;
        private void Start()
        {
            // Starts the timer automatically
            timeText = GetComponent<TextMeshProUGUI>();
            healthScript = GetComponentInParent<HealthScript>();
        }
        void LateUpdate()
        {
            if (time < 25)
            {
                time += Time.deltaTime;
                if (time >= 45)
                    timerIsRunning = true;
            }

            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 10;
                    timerIsRunning = false;
                    healthScript.PlayDeath();
                }
            }
        }
        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            float milliseconds = (timeToDisplay % 1) * 1000;
            if (minutes >= 0 && seconds >= 0 && milliseconds >= 0)
                timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            else timeText.text = string.Format("00:00:000");
        }
    }
}
