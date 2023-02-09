using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Localizations;
using UnityEngine;

namespace _Project.Scripts.Main.Settings
{
    [CreateAssetMenu(menuName = "Custom/Settings/Game Settings")]
    public class GameSettings : SettingsSO
    {
        public Locales CurrentLocale;
        
        [Range(Attributes.SensitivityMin,Attributes.SensitivityMax)] 
        public float Sensitivity;
        
        public override void ApplySettings(SettingsService settingsService) {}
        
        public static class Attributes
        {
            public const float SensitivityMin = 0.1f;
            public const float SensitivityMax = 1f;
        }
    }
}