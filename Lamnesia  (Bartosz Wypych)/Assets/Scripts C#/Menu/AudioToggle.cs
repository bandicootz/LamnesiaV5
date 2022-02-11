using UnityEngine;
using UnityEngine.UI;
using Lamnesia.InGame.Managers;

namespace Lamnesia.InGame.Menu
{
    public class AudioToggle : MonoBehaviour
    {
        private Toggle m_Toggle;

        private void Awake()
        {
            m_Toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            switch (m_Toggle.name)
            {
                case "SoundToggle":
                    m_Toggle.onValueChanged.AddListener((x) => AudioManager.Instance.SetSoundState(m_Toggle.isOn));
                    break;
                case "MusicToggle":
                    m_Toggle.onValueChanged.AddListener((x) => AudioManager.Instance.SetMusicState(m_Toggle.isOn));
                    break;
            }
        }

        private void OnDisable()
        {
            switch (m_Toggle.name)
            {
                case "SoundToggle":
                    m_Toggle.onValueChanged.RemoveListener((x) => AudioManager.Instance.SetSoundState(m_Toggle.isOn));
                    break;
                case "MusicToggle":
                    m_Toggle.onValueChanged.RemoveListener((x) => AudioManager.Instance.SetMusicState(m_Toggle.isOn));
                    break;
            }
        }
    }
}