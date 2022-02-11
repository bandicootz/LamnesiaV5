using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lamnesia.InGame.Menu
{
    public class DifficultyChanger : MonoBehaviour
    {
        private Button m_Btn;
        private Image m_Image;
        private TextMeshProUGUI m_Text;
        public string difficulty;

        private void Awake()
        {
            m_Btn = GetComponent<Button>();
            m_Image = GetComponentInChildren<Image>();
            m_Text = GetComponent<TextMeshProUGUI>();

            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Difficulty")))
            {
                SetDifficulty(PlayerPrefs.GetString("Difficulty"));
            }
            else
            {
                PlayerPrefs.SetString("Difficulty", difficulty);
            }
        }

        private void OnEnable()
        {
            m_Btn.onClick.AddListener(ChangeDifficulty);
        }

        private void ChangeDifficulty()
        {
            switch (difficulty)
            {
                case "easy":
                    SetDifficulty("medium");
                    m_Image.color = Color.yellow;
                    break;
                case "medium":
                    SetDifficulty("hard");
                    m_Image.color = Color.red;
                    break;
                case "hard":
                    SetDifficulty("easy");
                    m_Image.color = Color.green;
                    break;
            }
        }

        private void SetDifficulty(string _difficulty)
        {
            PlayerPrefs.SetString("Difficulty", _difficulty);
            m_Text.text = _difficulty;
            difficulty = _difficulty;
        }
    }
}