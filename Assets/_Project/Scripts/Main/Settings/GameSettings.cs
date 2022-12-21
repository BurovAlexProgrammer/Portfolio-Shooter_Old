using _Project.Scripts.Main.Localizations;
using UnityEngine;

namespace _Project.Scripts.Main.Settings
{
    [CreateAssetMenu(menuName = "Custom/Settings/Game Settings")]
    public class GameSettings : SettingsSO
    {
        public Locales CurrentLocale;
        
        public override void ApplySettings() {}
    }
}