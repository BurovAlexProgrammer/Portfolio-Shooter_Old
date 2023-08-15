﻿using UnityEngine;
using UnityEngine.Audio;

namespace Main.Service
{
    public class AudioServiceInstaller : BaseServiceInstaller
    {
        public AudioListener AudioListener;
        public AudioSource MusicAudioSource;
        public AudioMixerGroup SoundEffectMixerGroup;
        public AudioMixerGroup MusicMixerGroup;
        public AudioClip[] BattlePlaylist;
        public AudioClip[] MenuPlaylist;
    }
}