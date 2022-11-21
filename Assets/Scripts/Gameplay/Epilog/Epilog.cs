using UnityEngine;
using GJ2022.Gameplay.CharacterMovement;
using System.Collections;
using System;
using GJ2022.Global.PubSub;

namespace GJ2022.Gameplay.Epilog
{
    public class Epilog : MonoBehaviour
    {
        [SerializeField] private Transform _ghostPost;
        [SerializeField] private Transform _toJump;

        [SerializeField] private NPCMovement _npc;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] private NPCMovePack[] _movePath;

        bool _isTimePlayerToMove;
        bool _isPlayerDoneMove;
        

        private void Update()
        {
            if (!_isTimePlayerToMove)
                return;
            if (_isPlayerDoneMove)
                return;
            if (_player.transform.position.x < _toJump.position.x)
            {
                _player.Move(PlayerMovement.PlayerFacing.RIGHT);
            }
            else
            {
                _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _isPlayerDoneMove = true;
                Invoke(nameof(JumpNow), .5f);
            }
        }
        private void JumpNow()
        {
            print("jump");
            //_player.Move(PlayerMovement.PlayerFacing.RIGHT);
            _player.CutSceneJump();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                //_npc.gameObject.SetActive(true);
                //_npc.transform.position = _ghostPost.position;

                StartCoroutine(WaitSendSignal(() =>
                {
                    print("Jump");
                    _npc.JumpCutScene();
                }, 1f));
                //_npc.MoveNPCCharacter(_movePath, () => { });

                _player._isInCutScene = true;

                StartCoroutine(WaitSendSignal(() =>
                {
                    EventConnector.Publish("OnPlayerDialogue", new PlayerDialogueMessage(2, 2f));
                }, 1.5f));

                StartCoroutine(WaitSendSignal(() =>
                {
                    _isTimePlayerToMove = true;
                }, 2f));
            }
        }
        IEnumerator WaitSendSignal(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }

    }
}

