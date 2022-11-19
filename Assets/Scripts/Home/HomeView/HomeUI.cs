using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GJ2022.Global.SaveLoad;

namespace GJ2022.Home.HomeView
{
    public class HomeUI : MonoBehaviour
    {
        [Header("Home Menu Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;

        [Header("Scene Manager")]
        [SerializeField] private string _gameplaySceneName;

        [Header("Home Pop Up")]
        [SerializeField] private GameObject _settingsPopUp;
        [SerializeField] private GameObject _creditsPopUp;
        [SerializeField] private GameObject _warningPopUp;
        [SerializeField] private GameObject _warningExitApplication;

        private bool _isNewPlayer;

        private void Awake()
        {
            _isNewPlayer = SaveSystem.Instance.IsPlayerNew();
            if (_isNewPlayer)
            {
                _continueButton.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            RemoveAllButtonListeners();
            _startButton.onClick.AddListener(OnStartButton);
            _continueButton.onClick.AddListener(OnContinueButton);
            _settingsButton.onClick.AddListener(OnSettingsButton);
            _creditsButton.onClick.AddListener(OnCreditsButton);
            _exitButton.onClick.AddListener(OnExitButton);
        }
        private void OnDisable()
        {
            RemoveAllButtonListeners();
        }

        private void RemoveAllButtonListeners()
        {
            _startButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _creditsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OnStartButton()
        {
            if (!_isNewPlayer)
                _warningPopUp.SetActive(true);
            else
                SceneManager.LoadScene(_gameplaySceneName);
        }
        private void OnContinueButton()
        {
            SceneManager.LoadScene(_gameplaySceneName);
        }
        private void OnSettingsButton()
        {
            _settingsPopUp.SetActive(true);
        }
        private void OnCreditsButton()
        {
            _creditsPopUp.SetActive(true);
        }
        private void OnExitButton()
        {
            _warningExitApplication.SetActive(true);
        }
    }
}
