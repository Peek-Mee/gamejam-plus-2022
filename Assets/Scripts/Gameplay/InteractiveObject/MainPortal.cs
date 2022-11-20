using UnityEngine;
using UnityEngine.Events;
using GJ2022.Global.PubSub;
using GJ2022.Global.SaveLoad;

namespace GJ2022.Gameplay.InteractiveObject
{
    [RequireComponent(typeof(Collider2D))]
    public class MainPortal : MonoBehaviour
    {
        [SerializeField] private int _orbsToOpen;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private SidePortal _otherSidePortal;

        private bool _isPortalOpened = false;
        private Collider2D _collider2d;
        private Vector3 _defaultScale;

        private UnityAction<object> _onOrbObtained;

        private void Awake()
        {
            _defaultScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() > _orbsToOpen)
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
                
                TweenClosePortal(collision.gameObject);
            }
        }

        private void OnOrbObatined(object message)
        {
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() >= _orbsToOpen)
            {
                _isPortalOpened = true;
                _collider2d.enabled = true;
                TweenOpenPortal();
            }
        }

        private void TweenOpenPortal()
        {
            LeanTween.scale(gameObject, _defaultScale, 1.5f).setOnUpdateVector3( val =>
            {
                transform.localScale = val;
            });
        }
        private void TweenClosePortal(GameObject go)
        {
            LeanTween.scale(gameObject, Vector3.zero, 1.5f).setOnUpdateVector3(val =>
            {
                transform.localScale = val;
            }).setOnComplete(() =>
            {
                go.SetActive(false);
                go.transform.position = _otherSidePortal.transform.position;
                _particleSystem.Stop(true);
                _otherSidePortal.Teleport();

                LeanTween.value(0f, 1.0f, 1.5f).setOnComplete(() =>
                {
                    go.SetActive(true);
                    gameObject.SetActive(false);
                });
            });
        }

    }

}
