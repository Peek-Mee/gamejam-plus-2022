using UnityEngine;
using GJ2022.Global.SaveLoad;
using System.Collections;

namespace GJ2022.Gameplay.GameFlow
{
    [RequireComponent(typeof(Collider2D))]
    public class NotEnoughOrb : MonoBehaviour
    {
        [SerializeField] private int _minimumOrbToPass;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private float _delayToWarn;
        private bool _isInDelay;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isInDelay)
                return;
            if (SaveSystem.Instance.GetPlayerData().TotalOrbsCollected() < _minimumOrbToPass)
                StartCoroutine(Warn());

        }
        private IEnumerator Warn()
        {
            _isInDelay = true;
            yield return new WaitForSeconds(_delayToWarn);
            _isInDelay = false;
            _gameOverPanel.SetActive(true);
        }
    }

}
