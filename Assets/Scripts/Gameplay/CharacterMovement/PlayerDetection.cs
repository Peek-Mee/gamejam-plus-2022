using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GJ2022.Global.PubSub;
using UnityEngine.Events;
using GJ2022.Gameplay.SavePoint;

namespace GJ2022.Gameplay.CharacterMovement
{
    public class PlayerDetection : MonoBehaviour
    {
        private UnityAction<object> _onPlayerNearby;
        private UnityAction<object> _onPlayerLeave;
        [SerializeField] private SavePointManager _savePointManager;
        private string _orbId;

        private bool _isPlayerNearbyOrb;

        private void Awake()
        {
            _onPlayerLeave = new(OnPlayerLeave);
            _onPlayerNearby = new(OnPlayerNearby);
        }

        private void OnEnable()
        {
            EventConnector.Subscribe("OnPlayerNearby", _onPlayerNearby);
            EventConnector.Subscribe("OnPlayerLeave", _onPlayerLeave);
        }

        private void Update()
        {
            if (!_isPlayerNearbyOrb)
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                _savePointManager.InteractWithOrb(_orbId);
                _isPlayerNearbyOrb = false;
            }
        }

        private void OnPlayerNearby(object message)
        {
            PlayerNearbyMessage _message = (PlayerNearbyMessage)message;
            _isPlayerNearbyOrb = true;
            _orbId = _message.OrbId;

        }
        private void OnPlayerLeave(object message)
        {
            PlayerLeaveMessage _message = (PlayerLeaveMessage)message;
            _isPlayerNearbyOrb = false;
        }
    }

}
