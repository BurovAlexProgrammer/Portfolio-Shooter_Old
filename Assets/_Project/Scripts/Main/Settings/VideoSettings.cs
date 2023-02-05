using System;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace _Project.Scripts.Main.Settings
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Settings/Video Settings")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing; //TODO impl
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion; //TODO impl
        public bool PostProcessDepthOfField;
        public bool PostProcessFilmGrain;
        public bool PostProcessLensDistortion;
        public bool PostProcessMotionBlur;

        private VolumeProfile _volumeProfile;
        private VolumeComponent _volumeComponent;

        public override void ApplySettings(SettingsService settingsService)
        {
            _volumeProfile = settingsService.ScreenService.VolumeProfile;
            var components = settingsService.ScreenService.VolumeProfile.components;
            
            SetVolumeActive(typeof(Bloom), PostProcessBloom);
            SetVolumeActive(typeof(DepthOfField), PostProcessDepthOfField);
            SetVolumeActive(typeof(Vignette), PostProcessVignette);
            SetVolumeActive(typeof(FilmGrain), PostProcessFilmGrain);
            SetVolumeActive(typeof(MotionBlur), PostProcessMotionBlur);
            SetVolumeActive(typeof(LensDistortion), PostProcessLensDistortion);
            
            // postProcessLayer.antialiasingMode = PostProcessAntiAliasing ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : None;
        }

        private void SetVolumeActive(Type type, bool state)
        {
            if (_volumeProfile.TryGet(type, out _volumeComponent))
            {
                _volumeComponent.active = state;
                return;
            }

            throw new Exception($"VolumeProfile {type.FullName} not found.");
        }
    }

    public static class VideoSettingsAttributes
    {
        
    }
}