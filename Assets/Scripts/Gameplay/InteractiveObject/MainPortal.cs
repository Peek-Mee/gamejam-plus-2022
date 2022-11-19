using UnityEngine;
using UnityEngine.Events;
using GJ2022.Global.PubSub;
using GJ2022.Global.SaveLoad;

namespace GJ2022.Gameplay.InteractiveObject
{
    [RequireComponent(typeof(Collider2D))]
    public class MainPortal : MonoBehaviour
    {
        [SerializeField] private Vector2 _teleportPosition;
        [SerializeField] private int _orbsToOpen;
        [SerializeField] GameObject _portalView;
        private bool _isPortalOpened = false;
        private Collider2D _collider2d;

        private UnityAction<object> _onOrbObtained;
        private void Awake()
        {
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() >= _orbsToOpen)
            {
                gameObject.SetActive(false);
                return;
            }
            _collider2d = GetComponent<Collider2D>();
            _collider2d.enabled = false;
        }
        private void OnEnable()
        {
            _onOrbObtained = new(OnOrbObatined);
            EventConnector.Subscribe("OnOrbObtained", _onOrbObtained);
        }
        private void OnDisable()
        {
            EventConnector.Unsubscribe("OnOrbObtained", _onOrbObtained);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!_isPortalOpened)
                return;
            if (!collision.CompareTag("Player"))
                return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.gameObject.transform.position = _teleportPosition;
                gameObject.SetActive(false);
            }
        }

        private void OnOrbObatined(object message)
        {
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() >= _orbsToOpen)
            {
                _isPortalOpened = true;
                _portalView.SetActive(true);
                _collider2d.enabled = true;
            }
        }

    }

}
