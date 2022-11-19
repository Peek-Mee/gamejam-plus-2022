using UnityEngine;

namespace GJ2022.Global.SaveLoad
{
    [System.Serializable]
    public class AudioSettingData
    {
        [SerializeField] private bool _isMasterMute;
        [SerializeField] private bool _isSfxMute;
        [SerializeField] private bool _isBgmMute;
        [SerializeField] private float _masterVolume;
        [SerializeField] private float _sfxVolume;
        [SerializeField] private float _bgmVolume;

        public AudioSettingData()
        {
            _isMasterMute = false;
            _isSfxMute = false;
            _isBgmMute = false;
            _masterVolume = 1.0f;
            _sfxVolume = 0.9375f;
            _bgmVolume = 0.8125f;
        }

        public bool IsMasterMute() { return _isMasterMute; }
        public bool IsSfxMute() { return _isSfxMute; }
        public bool IsBgmMute() { return _isBgmMute; }

        public void SetSfxMute(bool value) { _isSfxMute = value; }
        public void SetMasterMute(bool value) { _isMasterMute = value; }
        public void SetBgmMute(bool value) { _isBgmMute = value; }

        public float GetMasterVolume() { return _masterVolume; }
        public float GetSfxVolume() { return _sfxVolume; }
        public float GetBgmVolume() { return _bgmVolume; }

        public void SetMasterVolume(float value) { _masterVolume = value; }
        public void SetSfxVolume(float value) { _sfxVolume = value; }
        public void SetBgmVolume(float value) { _bgmVolume = value; }
    }
}
