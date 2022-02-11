using UnityEngine;
using UnityEngine.UI;

namespace Lamnesia.InGame.Menu
{
    public class SettingsCloseButton : MonoBehaviour
    {
        private Image m_Image;
        private Button m_Button;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            m_Button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            m_Button.onClick.AddListener(CloseSettings);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(CloseSettings);
        }

        private void Start()
        {
            m_Button.targetGraphic = m_Image;
        }

        private void CloseSettings()
        {
            gameObject.SetActive(false);
        }
    }
}
