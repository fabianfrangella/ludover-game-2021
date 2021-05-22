using UnityEngine.Audio;
using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {

        public Sound[] sounds;

        private void Awake()
        {
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
            }

        }


        public void Play(string name)
        {
            Debug.Log("Play " + name);
            var s = Array.Find(sounds, sound => sound.name.Equals(name));
            s.source.Play();
        }
        private void Update()
        {
        }
    }
}
