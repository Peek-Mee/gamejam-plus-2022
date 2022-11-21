using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeContinued : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _gameOverPanel.SetActive(true);
            
        }
    }
}
