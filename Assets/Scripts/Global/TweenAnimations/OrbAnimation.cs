using UnityEngine;

namespace GJ2022.Global.TweenAnimation
{
    public class OrbAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        public void OrbInteracted()
        {
            LeanTween.scale(gameObject, Vector3.zero, .75f);
        }
    }

}
