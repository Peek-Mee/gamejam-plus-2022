using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GJ2022.Gameplay.GameFlow
{
    public class PauseUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _exitPanelButton;

        [Header("Pop Ups")]
        [SerializeField] private GameObject _settingsPopUp;

        [Header("Scene Manager")]
        [SerializeField] private string _homeSceneName;

        private void OnEnable()
        {
            RemoveAllButtonListeners();
            _settingsButton.onClick.AddListener(OnSettingButton);
            _quitButton.onClick.AddListener(OnQuitButton);
            _exitPanelButton.onClick.AddListener(OnExitPanelButton);
        }

        private void OnDisable()
        {
            RemoveAllButtonListeners();
        }

        private void RemoveAllButtonListeners()
        {
            _settingsButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
            _exitPanelButton.onClick.RemoveAllListeners();
        }

        private void OnSettingButton()
        {
            _settingsPopUp.SetActive(true);
        }
        private void OnQuitButton()
        {
            SceneManager.LoadScene(_homeSceneName);
        }
        private void OnExitPanelButton()
        {
            gameObject.SetActive(false);
        }
    }

}
