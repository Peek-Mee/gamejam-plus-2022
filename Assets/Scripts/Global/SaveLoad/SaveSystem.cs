using UnityEngine;

namespace GJ2022.Global.SaveLoad
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        private PlayerData _playerData;
        private AudioSettingData _audioSettingData;

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

            _playerData = new();
            _audioSettingData = new();

            LoadPreviousPlayerData();
            LoadPreviousAudioSettingData();
            DontDestroyOnLoad(gameObject);
        }

        private void LoadPreviousPlayerData()
        {
            // Player Data Load
            if (!PlayerPrefs.HasKey("PlayerData"))
            {
                SavePlayerProgress();
            }

            _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));

        }
        private void LoadPreviousAudioSettingData()
        {
            if (!PlayerPrefs.HasKey("AudioSettingData"))
            {
                SaveAudioSetting();
            }

            _audioSettingData = JsonUtility.FromJson<AudioSettingData>(PlayerPrefs.GetString("AudioSettingData"));
        }

        public bool IsPlayerNew()
        {
            if (_playerData.GetLastSavePointId() == "") return true;
            return false;
        }

        public void SavePlayerProgress()
        {
            PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(_playerData));
            PlayerPrefs.Save();
        }
        public void SaveAudioSetting()
        {
            PlayerPrefs.SetString("AudioSettingData", JsonUtility.ToJson(_audioSettingData));
            PlayerPrefs.Save();
        }

        public PlayerData GetPlayerData()
        {
            return _playerData;
        }
        public AudioSettingData GetAudioSettingData()
        {
            return _audioSettingData;
        }

        public void ResetPlayerProgress()
        {
            _playerData = new();
            SavePlayerProgress();
        }
    }

}
