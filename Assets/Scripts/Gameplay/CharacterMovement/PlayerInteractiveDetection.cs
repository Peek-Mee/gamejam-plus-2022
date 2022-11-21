using UnityEngine;
using GJ2022.Gameplay.InteractiveObject;

namespace GJ2022.Gameplay.CharacterMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInteractiveDetection : MonoBehaviour
    {
        [SerializeField] private LayerMask _interactiveObjectMask;
        [SerializeField] private float _distanceDetection = 1.0f;
        private bool _isInteracting = false;
        private Rigidbody2D _playerRigidBody;
        private IInteractive _targetInteraction;
        [SerializeField] private GameObject _popUpDialogue;

        private void Awake()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            Physics2D.queriesStartInColliders = false;
            RaycastHit2D _raycastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, _distanceDetection, _interactiveObjectMask);

            
            if (_raycastHit.collider != null && !(_isInteracting) && Input.GetKeyDown(KeyCode.E))
            {
                _targetInteraction =  _raycastHit.collider.gameObject.GetComponent<IInteractive>();
                
                _targetInteraction?.PlayerInteracted(_playerRigidBody);
                _isInteracting = true;
            }
            else if (_isInteracting && Input.GetKeyDown(KeyCode.E))
            {
                _targetInteraction?.PlayerStopInteract();
                _isInteracting = false;
                _targetInteraction = null;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * _distanceDetection);
        }
    }
}

