using UnityEngine;


namespace GJ2022.Gameplay.SavePoint
{
    public class SavePointManager : MonoBehaviour
    {
        [SerializeField] private SavePointData[] _orbsCollection;

        public void InteractWithOrb(string orbId)
        {
            GameObject _orb = System.Array.Find(_orbsCollection, orb => orb.Id == orbId)?.GameObject;
            if (_orb != null)
                _orb.GetComponent<ISavePointObject>().Interact();
        }
        public Vector3 GetSpawnPosition(string orbId)
        {
            return System.Array.Find(_orbsCollection, orb => orb.Id == orbId).Position;
        }
    }

}
