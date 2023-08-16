using UnityEngine;

namespace Main.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}
