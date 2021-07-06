using System;
using UnityEngine;

namespace Audio
{
    public class ObjectAudioArray : MonoBehaviour
    {
        public Sound[] sounds;
        
        private void Start()
        {
            foreach (var sound in sounds)
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
            }
        }

        public void Play(string name)
        {
            var s = Array.Find(sounds, sound => sound.name.Equals(name));
            if (s == null)
            {
                Debug.Log("Sound " + name + " Not Found");
                return;
            }
            s.source.Play();
        }
    }
}