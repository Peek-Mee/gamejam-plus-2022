using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "Input")]
public class InputBinding : ScriptableObject
{
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private KeyCode _run;
    [SerializeField] private KeyCode _jump;

    public KeyCode MoveLeft => _moveLeft;
    public KeyCode MoveRight => _moveRight;
    public KeyCode Run => _run;
    public KeyCode Jump => _jump;
}
