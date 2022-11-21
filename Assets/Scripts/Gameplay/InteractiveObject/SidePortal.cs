using System.Collections;
using UnityEngine;
using GJ2022.Global.SaveLoad;
using GJ2022.Global.PubSub;

namespace GJ2022.Gameplay.InteractiveObject
{
    public class SidePortal : MonoBehaviour
    {
        private bool _isReadyToDispose = false;
        [SerializeField] private float _timeToDispose;
        [SerializeField] private ParticleSystem _particleSystem;
        private Vector3 _defaultScale;
        [SerializeField] private string _id;

        public void Teleport()
        {
            TweenOpenPortal();
        }

        private void Awake()
        {
            _defaultScale = transform.localScale;
            transform.localScale = Vector3.zero;
        }
        //private void Start()
        //{
        //    StartCoroutine(DisposeCountDown());
        //}

        public bool IsReadyToDispose()
        {
            return _isReadyToDispose;
        }

        private IEnumerator DisposeCountDown()
        {
            yield return new WaitForSeconds(_timeToDispose);
            TweenClosePortal();
            _isReadyToDispose = true;
        }

        private void TweenOpenPortal()
        {
            LeanTween.scale(gameObject, _defaultScale, 1.5f).setOnUpdateVector3(val =>
            {
                transform.localScale = val;
            }).setOnComplete(() =>
            {
                StartCoroutine(DisposeCountDown());
                SaveSystem.Instance.GetPlayerData().SetLastSavePoint(_id);
                SaveSystem.Instance.SavePlayerProgress();

                EventConnector.Publish("OnOrbObtained", new OrbObtainedMessage(_id));
            });
        }
        private void TweenClosePortal()
        {
            LeanTween.scale(gameObject, Vector3.zero, 1.5f).setOnUpdateVector3(val =>
            {
                transform.localScale = val;
            }).setOnComplete(() =>
            {
                _particleSystem.Stop(true);
                gameObject.SetActive(false);
            });
        }
    }

}
