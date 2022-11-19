using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GJ2022.Global.SaveLoad;

namespace GJ2022.Home.HomeView
{
    public class WarningUI : MonoBehaviour
    {
        [Header("Home Menu Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _closeButton;

        [Header("Scene Manager")]
        [SerializeField] private string _gameplaySceneName;

        private void OnEnable()
        {
            RemoveAllButtonListeners();
            _startButton.onClick.AddListener(OnStartButton);
            _closeButton.onClick.AddListener(OnCloseButton);
        }
        private void OnDisable()
        {
            RemoveAllButtonListeners();
        }

        private void RemoveAllButtonListeners()
        {
            _startButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void OnStartButton()
        {
            SaveSystem.Instance.ResetPlayerProgress();
            SceneManager.LoadScene(_gameplaySceneName);
        }
        private void OnCloseButton()
        {
            gameObject.SetActive(false);
        }
    }

}
