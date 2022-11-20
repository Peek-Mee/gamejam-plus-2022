using UnityEngine;

namespace GJ2022.Gameplay.GameFlow
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _pausePanel.SetActive(true);
        }
    }

}
