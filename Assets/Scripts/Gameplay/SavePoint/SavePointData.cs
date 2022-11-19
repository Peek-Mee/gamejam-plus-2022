using UnityEngine;

namespace GJ2022.Gameplay.SavePoint
{
    [System.Serializable]
    public class SavePointData
    {
        [SerializeField] private string _orbId;
        [SerializeField] private GameObject _orbObject;

        public string Id => _orbId;
        public Vector3 Position => _orbObject.transform.position;
        public GameObject GameObject => _orbObject;

    }
}
