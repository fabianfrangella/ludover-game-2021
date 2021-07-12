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
                sound.source.loop = sound.loop;
                if (sound.name.Equals("Theme") || sound.playOnPause) sound.source.ignoreListenerPause = true;
            }

        }

        private void Start()
        {
            Play("Theme");
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
