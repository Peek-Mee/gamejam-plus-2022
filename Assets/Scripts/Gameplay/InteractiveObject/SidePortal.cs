using System.Collections;
using UnityEngine;

namespace GJ2022.Gameplay.InteractiveObject
{
    public class SidePortal : MonoBehaviour
    {
        private bool _isReadyToDispose = false;
        [SerializeField] private float _timeToDispose;

        private void Start()
        {
            StartCoroutine(DisposeCountDown());
        }

        public bool IsReadyToDispose()
        {
            return _isReadyToDispose;
        }

        private IEnumerator DisposeCountDown()
        {
            yield return new WaitForSeconds(_timeToDispose);
            _isReadyToDispose = true;
        }
    }

}
