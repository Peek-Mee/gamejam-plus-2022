using UnityEngine;
using System;
using System.Collections.Generic;

namespace GJ2022.Global.SaveLoad
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private Guid _playerId;
        [SerializeField] private string _lastSavePointId;
        [SerializeField] private List<string> _orbsCollected;

        public string GetLastSavePointId()
        {
            return _lastSavePointId;
        }

        public PlayerData()
        {
            _playerId = Guid.NewGuid();
            _orbsCollected = new();
        }
        public void SetLastSavePoint(string savePointId)
        {
            _lastSavePointId = savePointId;
            if (!_orbsCollected.Contains(savePointId))
                _orbsCollected.Add(savePointId);
        }

        public bool IsOrbsObtained(string orbsId)
        {
            return _orbsCollected.Contains(orbsId);
        }

        public int TotalOrbsCollected()
        {
            return _orbsCollected.Count;
        }
    }

}
   
