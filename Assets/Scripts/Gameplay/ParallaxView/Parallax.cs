using UnityEngine;

namespace GJ2022.Gameplay.ParallaxView
{
    //[RequireComponent(typeof(SpriteRenderer))]
    public class Parallax : MonoBehaviour
    {
        private float _length, _startPosition, _cameraStartPosition;
        [SerializeField] private GameObject _cameraToFollow;
        [SerializeField] private float _parallaxFactor = .01f;
        [SerializeField] private bool _isInfinitive;

        private void Awake()
        {
            _startPosition = transform.position.x;
            _cameraStartPosition = _cameraToFollow.transform.position.x;
        }

        private void Update()
        {
            var _deltaPosition = _cameraToFollow.transform.position.x - _cameraStartPosition;
            var _newPosition = transform.position;

            _newPosition.x = _startPosition - (_deltaPosition * _parallaxFactor);
            transform.position = _newPosition;
        }

        //private void Start()
        //{
        //    _startPosition = transform.position.x;
        //    _length = GetComponent<SpriteRenderer>().bounds.size.x;
        //}
        //private void FixedUpdate()
        //{
        //    float _cameraPosition = _cameraToFollow.transform.position.x;
        //    float _relativeDistance = _cameraPosition * _parallaxValue;
        //    float _tempRelativeDistance = _cameraPosition * (1 - _parallaxValue);

        //    Vector3 _changePosition = transform.position;
        //    _changePosition.x = _startPosition + _relativeDistance;

        //    transform.position = _changePosition;

        //    if (_isInfinite)
        //    {
        //        _ = _tempRelativeDistance > (_startPosition + _length) ? _startPosition += _length :
        //            _tempRelativeDistance < (_startPosition - _length) ? _startPosition -= _length : 0;
        //    }

        //}

    }
}

