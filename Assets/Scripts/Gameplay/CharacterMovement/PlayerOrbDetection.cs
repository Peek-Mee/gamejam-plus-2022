using UnityEngine;
using UnityEngine.Events;
using GJ2022.Global.PubSub;
using GJ2022.Gameplay.SavePoint;
using System.Collections;

namespace GJ2022.Gameplay.CharacterMovement
{
    public class PlayerOrbDetection : MonoBehaviour
    {
        private UnityAction<object> _onPlayerNearby;
        private UnityAction<object> _onPlayerLeave;
        private UnityAction<object> _onPlayerDialogue;
        private UnityAction<object> _onPlayerOutDialogue;
        [SerializeField] private SavePointManager _savePointManager;
        [SerializeField] private GameObject _popUpDialogue;
        [SerializeField] private Sprite[] _dialogues;
        private string _orbId;

        private bool _isPlayerNearbyOrb;

        private void Awake()
        {
            _onPlayerLeave = new(OnPlayerLeave);
            _onPlayerNearby = new(OnPlayerNearby);
            _onPlayerDialogue = new(OnPlayerDialogue);
            _onPlayerOutDialogue = new(OnPlayerOutDialogue);
        }


        private void OnEnable()
        {
            EventConnector.Subscribe("OnPlayerNearby", _onPlayerNearby);
            EventConnector.Subscribe("OnPlayerLeave", _onPlayerLeave);
            EventConnector.Subscribe("OnPlayerDialogue", _onPlayerDialogue);
            EventConnector.Subscribe("OnPlayerOutDialogue", _onPlayerOutDialogue);
        }

        private void OnDisable()
        {
            EventConnector.Unsubscribe("OnPlayerNearby", _onPlayerNearby);
            EventConnector.Unsubscribe("OnPlayerLeave", _onPlayerLeave);
            EventConnector.Unsubscribe("OnPlayerDialogue", _onPlayerDialogue);
            EventConnector.Unsubscribe("OnPlayerOutDialogue", _onPlayerOutDialogue);
        }

        private void Update()
        {
            if (!_isPlayerNearbyOrb)
                return;
            print(_isPlayerNearbyOrb);
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("interact with orb");
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
        private void OnPlayerDialogue(object message)
        {
            PlayerDialogueMessage _msg = (PlayerDialogueMessage)message;
            StartCoroutine(DisplayDialogue(_msg.Id, _msg.DisposeAfter));
        }
        private void OnPlayerOutDialogue(object message)
        {
            _popUpDialogue.SetActive(false);
        }

        IEnumerator DisplayDialogue(int id, float disposeAfter)
        {
            _popUpDialogue.GetComponent<SpriteRenderer>().sprite = _dialogues[id];
            _popUpDialogue.SetActive(true);
            yield return new WaitForSeconds(disposeAfter);
            _popUpDialogue.SetActive(false);
        }
    }

}
