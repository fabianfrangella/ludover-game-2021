using UnityEngine.Audio;
using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {

        public Sound[] sounds;

        private readonly string[] skeletonSounds =
        {
            "FootstepSkeleton1", 
            "FootstepSkeleton2", 
            "FootstepSkeleton3", 
            "FootstepSkeleton4", 
            "FootstepSkeleton5"
        };

        private void Awake()
        {
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.spatialBlend = Array.Exists(skeletonSounds, s => s.Equals(sound.name)) ? 1 : 0.5f;
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
