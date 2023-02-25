using _Project.Scripts.Main.AppServices;

namespace _Project.Scripts.Main.Settings
{
    public interface ISettingGroup
    {
        void Init(SettingsService settingsService);
        void LoadFromFile();
        void SaveToFile();
        void Cancel();
        void Restore();
        void ApplySettings(SettingsService settingsService);
    }
}