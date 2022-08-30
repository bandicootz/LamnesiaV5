using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Lamnesia.Player
{
    public class YourTime : MonoBehaviour
    {
        TextMeshProUGUI text;
        public Timer timer;

        public float time;
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        public void GetTime()
        {
            time = 600 - timer.timeRemaining;
            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);
            float milliseconds = (time % 1) * 1000;
            if (minutes >= 0 && seconds >= 0 && milliseconds >= 0)
                text.text += string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            else text.text += string.Format("00:00:000");
        }
    }
}