using UnityEngine;

namespace _Project.Scripts.Main.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}
