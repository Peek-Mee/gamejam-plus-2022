using UnityEngine;
using GJ2022.Global.SaveLoad;
using GJ2022.Global.PubSub;

namespace GJ2022.Gameplay.SavePoint
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SavePointObject : MonoBehaviour, ISavePointObject
    {
        [SerializeField] private string _orbsId;
        [SerializeField] private GameObject _popUp;
        bool _isInteracted;

        private void Start()
        {
            if (SaveSystem.Instance.GetPlayerData().IsOrbsObtained(_orbsId))
                gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventConnector.Publish("OnPlayerNearby", new PlayerNearbyMessage(_orbsId));
                if (!_isInteracted)
                {
                    _isInteracted = true;
                    if (SaveSystem.Instance.IsPlayerNew())
                        EventConnector.Publish("OnPlayerDialogue", new PlayerDialogueMessage(1, 2.5f));
                }
                _popUp.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventConnector.Publish("OnPlayerLeave", new PlayerLeaveMessage(_orbsId));
                _popUp.SetActive(false);
            }
        }
        public void Interact()
        {

            Interacted();
        }

        private void Interacted()
        {
            _popUp.SetActive(false);
            LeanTween.scale(gameObject, Vector3.zero, .75f).setOnComplete(() =>
            {
                SaveSystem.Instance.GetPlayerData().SetLastSavePoint(_orbsId);
                SaveSystem.Instance.SavePlayerProgress();
                EventConnector.Publish("OnOrbObtained", new OrbObtainedMessage(_orbsId));
                gameObject.SetActive(false);
            });
            
        }
    }
}
