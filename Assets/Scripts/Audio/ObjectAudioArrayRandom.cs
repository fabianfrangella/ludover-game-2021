using UnityEngine;

namespace Audio
{
    public class ObjectAudioArrayRandom : ObjectAudioArray
    {
        public void Play() 
        {
            var sound = sounds[Random.Range(0, sounds.Length)];
            sound.source.spatialBlend = 0;
            sound.source.Play();
        }
    }
}