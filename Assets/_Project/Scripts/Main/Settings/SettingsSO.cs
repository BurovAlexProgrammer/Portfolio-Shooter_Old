using Main.Services;
using UnityEngine;

namespace Main.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings(SettingsService settingsService);
    }
}