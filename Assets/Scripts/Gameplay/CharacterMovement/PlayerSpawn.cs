using UnityEngine;
using GJ2022.Global.SaveLoad;
using GJ2022.Gameplay.SavePoint;

namespace GJ2022.Gameplay.CharacterMovement
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField] private SavePointManager _savePointManager;

        private void Start()
        {
            string _orbId = SaveSystem.Instance.GetPlayerData().GetLastSavePointId();
            if (SaveSystem.Instance.IsPlayerNew())
                return;
            var _spawnPosition = _savePointManager.GetSpawnPosition(_orbId);
            if (_spawnPosition != null)
            {
                if (_spawnPosition.x == float.NegativeInfinity)
                    return;
                if (_spawnPosition.x == float.PositiveInfinity)
                    return;
                transform.position = _spawnPosition;

            }
        }
    }
}
