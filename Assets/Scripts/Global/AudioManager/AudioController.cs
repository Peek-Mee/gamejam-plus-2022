using GJ2022.Global.SaveLoad;
using UnityEngine;
using UnityEngine.Audio;

namespace GJ2022.Global.AudioManager
{
    public enum AudioMixerType { MASTER, BGM, SFX }

    public class AudioController : MonoBehaviour
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixerGroup _masterMixer;
        [SerializeField] private AudioMixerGroup _bgmMixer;
        [SerializeField] private AudioMixerGroup _sfxMixer;

        
        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitAudioSetting();
        }

        private void InitAudioSetting()
        {
            var _audioSetting = SaveSystem.Instance.GetAudioSettingData();
            if (_audioSetting.IsMasterMute())
                MuteAudio(AudioMixerType.MASTER);
            if (_audioSetting.IsBgmMute())
                MuteAudio(AudioMixerType.BGM);
            if (_audioSetting.IsSfxMute())
                MuteAudio(AudioMixerType.SFX);
        }

        public void MuteAudio(AudioMixerType type)
        {
            switch (type)
            {
                case AudioMixerType.MASTER:
                    _masterMixer.audioMixer.SetFloat("MASTER", -80f);
                    break;
                case AudioMixerType.BGM:
                    _bgmMixer.audioMixer.SetFloat("BGM", -80f);
                    break;
                case AudioMixerType.SFX:
                    _sfxMixer.audioMixer.SetFloat("SFX", -80f);
                    break;
            }
        }

        public void UnMuteAudio(AudioMixerType type)
        {
            var _audioSetting = SaveSystem.Instance.GetAudioSettingData();
            float _volume = type switch
            {
                AudioMixerType.MASTER => _audioSetting.GetMasterVolume(),
                AudioMixerType.BGM => _audioSetting.GetBgmVolume(),
                AudioMixerType.SFX => _audioSetting.GetSfxVolume(),
                _ => 0f
            };
            _volume = NormalizeVolumeValue(_volume);

            switch (type)
            {
                case AudioMixerType.MASTER:
                    _masterMixer.audioMixer.SetFloat("MASTER", _volume);
                    break;
                case AudioMixerType.BGM:
                    _bgmMixer.audioMixer.SetFloat("BGM", _volume);
                    break;
                case AudioMixerType.SFX:
                    _sfxMixer.audioMixer.SetFloat("SFX", _volume);
                    break;
            }
        }
        private float NormalizeVolumeValue(float rawVolume)
        {
            return (rawVolume * 80f) - 80f;
        }

        public void UpdateMixerVolume(AudioMixerType type, float newVolumeValue)
        {
            var _normalizedVolume = NormalizeVolumeValue(newVolumeValue);

            switch (type)
            {
                case AudioMixerType.MASTER:
                    _masterMixer.audioMixer.SetFloat("MASTER", _normalizedVolume);
                    break;
                case AudioMixerType.BGM:
                    _bgmMixer.audioMixer.SetFloat("BGM", _normalizedVolume);
                    break;
                case AudioMixerType.SFX:
                    _sfxMixer.audioMixer.SetFloat("SFX", _normalizedVolume);
                    break;
            }
        }
    }

}
