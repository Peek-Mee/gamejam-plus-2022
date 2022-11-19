using UnityEngine;
using UnityEngine.UI;

namespace GJ2022.Home.Settings
{
    [RequireComponent(typeof(RectTransform))]
    public class SettingsUI : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private GameObject _content;
        [SerializeField] private GameObject _mainPanel;

        [Header("BGM")]
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Toggle _bgmToggle;

        [Header("SFX")]
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Toggle _sfxToggle;

        [Header("Buttons")]
        [SerializeField] private Button _backBtn;


        private Vector2 _minBeforeOpen;
        private Vector2 _maxBeforeOpen;
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _maxBeforeOpen = _rect.anchorMax;
            _minBeforeOpen = _rect.anchorMin;
        }
        private void OnEnable()
        {
            _backBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.AddListener(OnBackButton);
        }
        private void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();

        }

        private void OnBackButton()
        {
            gameObject.SetActive(false);
        }

    }

}
