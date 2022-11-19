using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ2022.Gameplay.InteractiveObject
{
    public interface IInteractive
    {
        public void PlayerInteracted(Rigidbody2D playerRigidBody);
        public void PlayerStopInteract();
    }
}
