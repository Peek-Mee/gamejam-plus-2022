using UnityEngine;

namespace GJ2022.Gameplay.CharacterMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputBinding _input;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _bottomCheck;

        [Header("Player Movement Stats")]
        [SerializeField] private float _walkingSpeed;
        [SerializeField] private float _runningSpeed;
        [SerializeField] private float _jumpHeight;

        public enum PlayerFacing { LEFT, RIGHT }
        private PlayerFacing _currentFacing = PlayerFacing.RIGHT;
        private bool _isRunning;
        private Rigidbody2D _rigidBody;
        private Vector3 _refVelocity = Vector3.zero;
        const float CHECK_RADIUS = .25f;
        private bool _isGrounded;
        public bool _isInCutScene;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            
        }
        private void FixedUpdate()
        {
            _isGrounded = false;

            Collider2D[] _colls = Physics2D.OverlapCircleAll(_bottomCheck.position, CHECK_RADIUS, _whatIsGround);
            for (int i = 0; i < _colls.Length; i++)
            {
                if (_colls[i].gameObject != gameObject)
                {
                    _isGrounded = true;
                }
            }
        }

        private void Update()
        {
            if (_isInCutScene)
                return;

            _ = Input.GetKey(_input.Run) ? _isRunning = true : _isRunning = false;
            
            if (_isGrounded)
            {
                if (Input.GetKeyDown(_input.Jump))
                {
                    Jump();
                }
                if (Input.GetKey(_input.MoveLeft))
                    Move(PlayerFacing.LEFT);
                if (Input.GetKey(_input.MoveRight))
                    Move(PlayerFacing.RIGHT);
            }
        }

        private void Jump()
        {
            _rigidBody.AddForce(new Vector2(0f, _jumpHeight));
        }
        private void Move(PlayerFacing direction)
        {
            if (_currentFacing != direction)
            {
                _currentFacing = direction;
                FlipCharacterFacing();
            }
            float _speed = _isRunning ? _runningSpeed : _walkingSpeed;
            _speed *= Time.deltaTime;
            Vector3 _vectorChange = Vector3.zero;

            switch (direction)
            {
                case PlayerFacing.LEFT:
                    _vectorChange.x = -1f * _speed * 10f ;
                    break;
                case PlayerFacing.RIGHT:
                    _vectorChange.x = 1f * _speed * 10f ;
                    break;
            }
            _vectorChange.y = _rigidBody.velocity.y;

            _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, _vectorChange, ref _refVelocity, 0.05f);

        }
       
        private void FlipCharacterFacing()
        {
            Vector3 _localScale = transform.localScale;
            _localScale.x *= -1;
            transform.localScale = _localScale;
        }
    }

}
   
