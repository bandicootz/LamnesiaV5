using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Lamnesia.InGame.Menu
{
    public class ButtonController : MonoBehaviour
    {
        private Button m_Btn;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject loadingScreen;

        private void Awake()
        {
            m_Btn = GetComponent<Button>();
        }

        private void OnEnable()
        {
            m_Btn.onClick.AddListener(() => AssignButton(m_Btn.name));
        }

        private void AssignButton(string btnName)
        {
            switch (btnName)
            {
                case "SettingsButton":
                    settingsPanel.SetActive(true);
                    break;
                case "StartButton":
                    SceneManager.LoadSceneAsync(2);
                    loadingScreen.SetActive(true);
                    break;
                case "CreditsButton":
                    creditsPanel.SetActive(true);
                    break;
                case "ExitButton":
                    Application.Quit();
                    break;
            }
        }

        //private void Update()
        //{
        //    if (loadingScreen !=null && loadingScreen.activeSelf)
        //            loadingScreen.GetComponent<Image>().fillAmount += (float)0.1; 
        //}
    }
}