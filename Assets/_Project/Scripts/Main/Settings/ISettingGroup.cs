using Main.Services;

namespace Main.Settings
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