using System.Collections;
using UnityEngine;

namespace GJ2022.Gameplay.InteractiveObject
{
    [RequireComponent(typeof(FixedJoint2D), typeof(Collider2D), typeof(Rigidbody2D))]
    public class PushPullableObject : MonoBehaviour, IInteractive
    {
        private FixedJoint2D _joint;
        private Rigidbody2D _rigidBody;
        [SerializeField] private PhysicsMaterial2D _material;
        [SerializeField] private GameObject _popUp;

        private void Awake()
        {
            _joint = GetComponent<FixedJoint2D>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        
        public void PlayerInteracted(Rigidbody2D playerRigidBody)
        {
            _joint.enabled = true;
            _joint.connectedBody = playerRigidBody;
            _rigidBody.mass = 1;
        }
        public void PlayerStopInteract()
        {
            _joint.enabled = false;
            _joint.connectedBody = null;
            _rigidBody.mass = 10;
        }
        private IEnumerator Wait(RigidbodyType2D type)
        {
            yield return new WaitForSeconds(.25f);
            _rigidBody.bodyType = type;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            _popUp.SetActive(true);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            _popUp.SetActive(false);
        }
    }
}
