using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GJ2022.Global.AudioManager
{
    public class AudioController : MonoBehaviour
    {
        [Header("Audio Mixers")]
        [SerializeField] private AudioMixerGroup _masterMixer;
        [SerializeField] private AudioMixerGroup _bgmMixer;
        [SerializeField] private AudioMixerGroup _sfxMixer;

        public static AudioController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }


    }

}
