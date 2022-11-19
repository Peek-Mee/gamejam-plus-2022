using UnityEngine;

namespace GJ2022.Gameplay.InteractiveObject
{
    [RequireComponent(typeof(FixedJoint2D), typeof(Collider2D), typeof(Rigidbody2D))]
    public class PushPullableObject : MonoBehaviour, IInteractive
    {
        private FixedJoint2D _joint;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _joint = GetComponent<FixedJoint2D>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        
        public void PlayerInteracted(Rigidbody2D playerRigidBody)
        {
            _joint.enabled = true;
            _joint.connectedBody = playerRigidBody;
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        public void PlayerStopInteract()
        {
            _joint.enabled = false;
            _joint.connectedBody = null;
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
