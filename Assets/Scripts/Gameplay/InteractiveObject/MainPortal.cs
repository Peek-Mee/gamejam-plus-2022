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
        [SerializeField] private GameObject _popUp;
        private bool _isInteracted;
        bool _isAllowToInteract = false;

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
            int _totalCollectedOrb = SaveSystem.Instance.GetPlayerData().TotalOrbsCollected();
            if (_totalCollectedOrb > _orbsToOpen)
            {
                gameObject.SetActive(false);
                return;
            }else if (_totalCollectedOrb == _orbsToOpen)
            {
                _collider2d = GetComponent<Collider2D>();
                _collider2d.enabled = true;
                TweenOpenPortal();

            }

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
            if (!_isPortalOpened && _isAllowToInteract)
                return;
            if (!collision.CompareTag("Player"))
                return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                _popUp.SetActive(false);
                TweenClosePortal(collision.gameObject);
            }
            

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            _isAllowToInteract = true;
            _popUp.SetActive(true);

            if (!_isInteracted)
            {
                _isInteracted = true;
                EventConnector.Publish("OnPlayerDialogue", new PlayerDialogueMessage(2, 2.5f));
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            _isAllowToInteract = false;

            _popUp.SetActive(false);
        }

        private void OnOrbObatined(object message)
        {
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() >= _orbsToOpen)
            {
                _isPortalOpened = true;
                GetComponent<Collider2D>().enabled = true;
                TweenOpenPortal();
            }
        }

        private void TweenOpenPortal()
        {
            LeanTween.scale(gameObject, _defaultScale, 1.5f).setOnUpdateVector3(val =>
           {
               transform.localScale = val;
           }).setOnComplete(() =>
           {
               _isPortalOpened = true;
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
