using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [Serializable]
    public class Sound 
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;

        public float distance = 10.5f;
        
        [HideInInspector]
        public AudioSource source;


    }
}