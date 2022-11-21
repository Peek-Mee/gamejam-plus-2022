using System;
using System.Collections;
using UnityEngine;
using GJ2022.Gameplay.CharacterMovement;
using GJ2022.Global.SaveLoad;
using GJ2022.Global.PubSub;

namespace GJ2022.Gameplay.Tutorial
{
    public class Prolog : MonoBehaviour
    {
        [SerializeField] private NPCMovement _npc;
        [SerializeField] private PlayerMovement _player;
        [SerializeField] NPCMovePack[] _movementPath;


        private void Start()
        {
            if (!SaveSystem.Instance.IsPlayerNew())
            {
                _player._isInCutScene = false;
                gameObject.SetActive(false);
                return;
            }
            _npc.MoveNPCCharacter(_movementPath, new Action(EnableUserInput));

            StartCoroutine(WaitSendSignal(() =>
            {
                EventConnector.Publish("OnPlayerDialogue", new PlayerDialogueMessage(0, 3f));
            }, 6.75f));

            //StartCoroutine(WaitSendSignal(() =>
            //{
            //    EventConnector.Publish("OnPlayerOutDialogue", new PlayerOutDialogueMessage());
            //}, 6f));
        }

        private void EnableUserInput()
        {
            _player._isInCutScene = false;
        }
        IEnumerator WaitSendSignal(Action callback, float delay)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }

}
