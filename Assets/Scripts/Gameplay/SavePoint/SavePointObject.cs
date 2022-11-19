using UnityEngine;
using GJ2022.Global.SaveLoad;
using GJ2022.Global.PubSub;

namespace GJ2022.Gameplay.SavePoint
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SavePointObject : MonoBehaviour, ISavePointObject
    {
        [SerializeField] private string _orbsId;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                EventConnector.Publish("OnPlayerNearby", new PlayerNearbyMessage(_orbsId));
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                EventConnector.Publish("OnPlayerLeave", new PlayerLeaveMessage(_orbsId));
        }
        public void Interact()
        {
            Interacted();
            gameObject.SetActive(false);
        }

        private void Interacted()
        {
            SaveSystem.Instance.GetPlayerData().SetLastSavePoint(_orbsId);
            SaveSystem.Instance.SavePlayerProgress();

            EventConnector.Publish("OnOrbInteracted", new OrbInteractedMessage(_orbsId));
        }
    }
}
