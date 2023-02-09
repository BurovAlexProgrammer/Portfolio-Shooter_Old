using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using UnityEngine;

namespace _Project.Scripts.Main.Audio
{
    [CreateAssetMenu(menuName = "Custom/Audio/Simple Audio Event")]
    public class SimpleAudioEvent : AudioEvent
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private RangedFloat _volume = new (0.8f, 1f);
        [SerializeField] [MinMaxRange(0,2)] private RangedFloat _pitch = new (0.8f, 1.2f);
        public override void Play(AudioSource audioSource)
        {
            if (_audioClips.Length == 0) throw new Exception("No audio clips on AudioEvent");

            audioSource.clip = _audioClips.GetRandomItem();
            audioSource.volume = _volume.GetRandomValue();
            audioSource.pitch = _pitch.GetRandomValue();
            audioSource.Play();
        }
    }
}