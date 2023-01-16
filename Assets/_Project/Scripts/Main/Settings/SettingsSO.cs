using _Project.Scripts.Main.Services;
using UnityEngine;

namespace _Project.Scripts.Main.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings(SettingsService settingsService);
    }
}