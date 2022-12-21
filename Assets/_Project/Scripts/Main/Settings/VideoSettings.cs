using System;
using _Project.Scripts.Main.Installers;
using _Project.Scripts.Main.Services;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Zenject;
using static UnityEngine.Rendering.PostProcessing.PostProcessLayer.Antialiasing;

namespace _Project.Scripts.Main.Settings
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Settings/Video Settings")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing;
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion;
        public bool PostProcessDepthOfField;
        public bool PostProcessChromaticAberration;
        public bool PostProcessLensDistortion;
        public bool PostProcessMotionBlur;

        [Inject]
        public void Construct(ScreenService screenService)
        {
            //TODO Research. Inject does not call, dont know why.
        }
        
        public override void ApplySettings()
        {
            var postProcessLayer = Services.Services.ScreenService.PostProcessLayer;   //Crutch instead of injection
            var postProcessVolume = Services.Services.ScreenService.PostProcessVolume; //Crutch instead of injection
            postProcessLayer.antialiasingMode = PostProcessAntiAliasing ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : None;
            foreach (var effectSettings in postProcessVolume.profile.settings)
            {
                switch (effectSettings)
                {
                    case AmbientOcclusion:
                        effectSettings.enabled.Override(PostProcessAmbientOcclusion);
                        break;
                    case Bloom:
                        effectSettings.enabled.Override(PostProcessBloom);
                        break;
                    case ChromaticAberration:
                        effectSettings.enabled.Override(PostProcessChromaticAberration);
                        break;
                    case DepthOfField:
                        effectSettings.enabled.Override(PostProcessDepthOfField);
                        break;
                    case LensDistortion:
                        effectSettings.enabled.Override(PostProcessLensDistortion);
                        break;
                    case MotionBlur:
                        effectSettings.enabled.Override(PostProcessMotionBlur);
                        break;
                    case Vignette:
                        effectSettings.enabled.Override(PostProcessVignette);
                        break;
                    case ColorGrading:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(effectSettings));
                }
            }
        }
    }

    public static class VideoSettingsAttributes
    {
        
    }
}