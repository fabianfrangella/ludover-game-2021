using System;
using UnityEngine;

namespace Audio
{
    public class ObjectAudio : MonoBehaviour
    {
        public Sound sound;

        private void Start()
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = 1;
            sound.source.rolloffMode = AudioRolloffMode.Linear;
            sound.source.minDistance = 1;
            sound.source.maxDistance = 10.5f;
            
            sound.source.Play();
        }
        
    }
}