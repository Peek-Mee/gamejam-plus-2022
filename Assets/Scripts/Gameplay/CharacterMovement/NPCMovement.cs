using UnityEngine;

namespace GJ2022.Gameplay.CharacterMovement
{

    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        public Vector3 CurrenntPosition => transform.position;
        public void MoveRight()
        {
            transform.Translate(_speed * Time.deltaTime * Vector2.right);
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
    }

}
