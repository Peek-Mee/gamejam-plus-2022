using UnityEngine;
using System;

namespace GJ2022.Global.SaveLoad
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private Guid _playerId;
        [SerializeField] private string _lastSavePointId;

        public Guid Id => _playerId;
        public string LastSavePointId => _lastSavePointId;

        public PlayerData()
        {
            _playerId = Guid.NewGuid();
        }
        public void SetLastSavePoint(string savePointId)
        {
            _lastSavePointId = savePointId;
        }
    }

}
   
