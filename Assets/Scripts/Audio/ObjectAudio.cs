using System;
using UnityEngine;

namespace Audio
{
    public class ObjectAudio : MonoBehaviour
    {
        public string clip;
        private AudioManager audioManager;

        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play(clip);
        }
        
    }
}