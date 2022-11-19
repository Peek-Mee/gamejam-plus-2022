using UnityEngine;
using UnityEngine.UI;
using GJ2022.Global.AudioManager;
using GJ2022.Global.SaveLoad;

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
            RemoveAllSettingsListeners();
            InitAudioSettingToUI();
            _backBtn.onClick.AddListener(OnBackButton);
        }
        private void OnDisable()
        {
            _backBtn.onClick.RemoveAllListeners();
            RemoveAllSettingsListeners();
        }

        private void OnBackButton()
        {
            gameObject.SetActive(false);
        }

        private void InitAudioSettingToUI()
        {
            SetAudioSettingToUI(AudioMixerType.BGM, _bgmSlider, _bgmToggle);
            SetAudioSettingToUI(AudioMixerType.SFX, _sfxSlider, _sfxToggle);
        }

        private void SetAudioSettingToUI(AudioMixerType type, Slider slider, Toggle toggle)
        {
            var _audioSetting = SaveSystem.Instance.GetAudioSettingData();

            bool _isMute = type switch
            {
                AudioMixerType.MASTER => _audioSetting.IsMasterMute(),
                AudioMixerType.BGM => _audioSetting.IsBgmMute(),
                AudioMixerType.SFX => _audioSetting.IsSfxMute(),
                _ => false
            };
            float _volume = type switch
            {
                AudioMixerType.MASTER => _audioSetting.GetMasterVolume(),
                AudioMixerType.BGM => _audioSetting.GetBgmVolume(),
                AudioMixerType.SFX => _audioSetting.GetSfxVolume(),
                _ => 0f
            };

            if (_isMute)
            {
                toggle.isOn = true;
                slider.interactable = false;
            }
            else
            {
                toggle.isOn = false;
                slider.interactable = true;
                slider.value = (1.0f - _volume);
                slider.onValueChanged.AddListener((float val) => { OnVolumeSliderChange(type, val); });
            }
            toggle.onValueChanged.AddListener((bool val) => { OnMuteToggleChange(type, val); });
        }

        private void OnVolumeSliderChange(AudioMixerType type, float value)
        {
            var _audioSetting = SaveSystem.Instance.GetAudioSettingData();

            var _value = 1.0f - value;
            var _type = type;

            switch (_type)
            {
                case AudioMixerType.MASTER:
                    _audioSetting.SetMasterVolume(_value);
                    break;
                case AudioMixerType.BGM:
                    _audioSetting.SetBgmVolume(_value);
                    break;
                case AudioMixerType.SFX:
                    _audioSetting.SetSfxVolume(_value);
                    break;
            }
            AudioController.Instance.UpdateMixerVolume(_type, _value);
            SaveSystem.Instance.SaveAudioSetting();
        }
        private void OnMuteToggleChange(AudioMixerType type, bool value)
        {
            var _audioSetting = SaveSystem.Instance.GetAudioSettingData();
            var _type = type;

            switch (_type)
            {
                case AudioMixerType.MASTER:
                    _audioSetting.SetMasterMute(value);
                    break;
                case AudioMixerType.BGM:
                    _audioSetting.SetBgmMute(value);
                    break;
                case AudioMixerType.SFX:
                    _audioSetting.SetSfxMute(value);
                    break;
            }

            if (value)
            {
                AudioController.Instance.MuteAudio(_type);
            }
            else
            {
                AudioController.Instance.UnMuteAudio(_type);
            }
            SaveSystem.Instance.SaveAudioSetting();
        }

        private void RemoveAllSettingsListeners()
        {
            _bgmSlider.onValueChanged.RemoveAllListeners();
            _bgmToggle.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.RemoveAllListeners();
            _sfxToggle.onValueChanged.RemoveAllListeners();
        }
    }

}
