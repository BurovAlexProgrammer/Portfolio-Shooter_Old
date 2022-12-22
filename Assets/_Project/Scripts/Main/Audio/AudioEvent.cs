using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{
    public abstract void Play(AudioSource _audioSource);
    public AudioSource audioSource;
}
