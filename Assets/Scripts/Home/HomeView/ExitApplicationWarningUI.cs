using UnityEngine;
using UnityEngine.UI;

namespace GJ2022.Home.HomeView
{
    public class ExitApplicationWarningUI : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _exitApplicationButton;

        private void OnEnable()
        {
            RemoveAllButtonListeners();
            _closeButton.onClick.AddListener(OnCloseButton);
            _exitApplicationButton.onClick.AddListener(OnExitApplicationButton);
        }

        private void OnDisable()
        {
            RemoveAllButtonListeners();
        }
        private void RemoveAllButtonListeners()
        {
            _closeButton.onClick.RemoveAllListeners();
            _exitApplicationButton.onClick.RemoveAllListeners();
        }
        private void OnCloseButton()
        {
            gameObject.SetActive(false);
        }
        private void OnExitApplicationButton()
        {
            Application.Quit();
        }
    }

}
