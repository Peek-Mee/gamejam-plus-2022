using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GJ2022.Global.SaveLoad;

namespace GJ2022.Gameplay.GameFlow
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private string _homeScene;
        [SerializeField] private string _gameplayScene;

        private void OnEnable()
        {
            RemoveAllButtonListeners();
            _homeButton.onClick.AddListener(OnHomeButton);
            _restartButton.onClick.AddListener(OnRestartButton);
        }
        private void OnDisable()
        {
            RemoveAllButtonListeners();
        }

        private void RemoveAllButtonListeners()
        {
            _homeButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
        }
        private void OnHomeButton()
        {
            SaveSystem.Instance.ResetPlayerProgress();
            SaveSystem.Instance.SavePlayerProgress();
            SceneManager.LoadScene(_homeScene);
        }
        private void OnRestartButton()
        {
            SaveSystem.Instance.ResetPlayerProgress();
            SaveSystem.Instance.SavePlayerProgress();
            SceneManager.LoadScene(_gameplayScene);
        }
    }
}

