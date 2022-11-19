using UnityEngine;

namespace GJ2022.Global.SaveLoad
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        private PlayerData _playerData;

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
            LoadPreviousData();
            DontDestroyOnLoad(gameObject);
        }

        private void LoadPreviousData()
        {
            if (!PlayerPrefs.HasKey("Player Data"))
            {
                SavePlayerProgress();
            }

            _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("Player Data"));

        }

        public bool IsPlayerNew()
        {
            if (_playerData.LastSavePointId == "") return true;
            return false;
        }

        public void SavePlayerProgress()
        {
            PlayerPrefs.SetString("Player Data", JsonUtility.ToJson(_playerData));
            PlayerPrefs.Save();
        }

        public PlayerData GetPlayerData()
        {
            return _playerData;
        }

        public void ResetPlayerProgress()
        {
            _playerData = new();
            SavePlayerProgress();
        }
    }

}
