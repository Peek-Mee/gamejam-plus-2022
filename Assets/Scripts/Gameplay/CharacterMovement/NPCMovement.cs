using System;
using System.Collections;
using UnityEngine;

namespace GJ2022.Gameplay.CharacterMovement
{
    [System.Serializable]
    public class NPCMovePack
    {
        [SerializeField] Vector3 _moveTo;
        [SerializeField] float _timeToMove;
        [SerializeField] LeanTweenType _ease;
        public Vector3 MoveTo => _moveTo;
        public float TimeToMove => _timeToMove;
        public LeanTweenType Ease => _ease;

    }
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private float _magnitude = .5f;
        [SerializeField] private float _frequency = 20f;
        [SerializeField] private float _speed;
        private bool _isAnimating;
        private PlayerMovement.PlayerFacing _currentFacing = PlayerMovement.PlayerFacing.RIGHT;


        public Vector3 CurrenntPosition => transform.position;
        public void MoveRight()
        {
            transform.Translate(_speed * Time.deltaTime * Vector2.right);
            ZigZag();
        }
        public void MoveLeft()
        {
            transform.Translate(_speed * Time.deltaTime * -Vector2.right);
        }
        public void MoveUp()
        {
            transform.Translate(_speed * Time.deltaTime * Vector2.up);
        }
        public void MoveDown()
        {
            transform.Translate(_speed * Time.deltaTime * -Vector2.up);
        }
        public void Dispose()
        {
            gameObject.SetActive(false);
        }

        private void ZigZag()
        {
            transform.Translate(Time.deltaTime * Mathf.Sin(Time.time * _frequency) *  _magnitude *Vector2.up);
        }

        public void MoveNPCCharacter(NPCMovePack[] packageMovement, Action callback)
        {
            gameObject.SetActive(true);
            float _deltaTime = 0f;
            for (int i = 0; i < packageMovement.Length; i++)
            {
                if (i == 0)
                {
                    StartCoroutine(MoveTo(packageMovement[0].MoveTo, packageMovement[0].TimeToMove, packageMovement[0].Ease, 0f, ()=> { }));
                    
                }else if ( i == packageMovement.Length - 1)
                {
                    StartCoroutine(MoveTo(packageMovement[i].MoveTo, packageMovement[i].TimeToMove, packageMovement[i].Ease, _deltaTime, () => 
                    {
                        callback.Invoke();
                        gameObject.SetActive(false);
                    }));
                }
                else
                {
                    StartCoroutine(MoveTo(packageMovement[i].MoveTo, packageMovement[i].TimeToMove, packageMovement[i].Ease, _deltaTime, () => { }));
                    
                }
                _deltaTime += packageMovement[0].TimeToMove;
            }
            //StartCoroutine(DisposeAfterAnimate(callback, _deltaTime));
        }
        private IEnumerator MoveTo(Vector3 to, float time, LeanTweenType ease, float delay, Action callback)
        {
            yield return new WaitForSeconds(delay);
            if (_currentFacing == PlayerMovement.PlayerFacing.LEFT && to.x - transform.position.x > 0)
            {
                FlipCharacterFacing();
                _currentFacing = PlayerMovement.PlayerFacing.RIGHT;
            }
            else if (_currentFacing == PlayerMovement.PlayerFacing.RIGHT && to.x - transform.position.x < 0)
            {
                FlipCharacterFacing();
                _currentFacing = PlayerMovement.PlayerFacing.LEFT;
            }
            if (ease == LeanTweenType.notUsed)
            {
                LeanTween.move(gameObject, to, time).setEaseInOutCubic().setOnComplete(callback);
            }
            else
            {
                LeanTween.moveX(gameObject, to.x, time).setEaseInOutCubic();
                LeanTween.moveY(gameObject, to.y, time).setEase(ease).setOnComplete(callback);

            }
            //
        }

        public void JumpCutScene()
        {
            GetComponent<Rigidbody2D>()?.AddForce(new Vector2(110f, 220f));
        }
        private IEnumerator DisposeAfterAnimate(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
            gameObject.SetActive(false);
        }
        private void FlipCharacterFacing()
        {
            Vector3 _localScale = transform.localScale;
            _localScale.x *= -1;
            transform.localScale = _localScale;
        }
    }

}
