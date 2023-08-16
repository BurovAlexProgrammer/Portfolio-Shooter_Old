using Main.Settings;

namespace Main.Services
{
    public class SettingsServiceInstaller : BaseServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
    }
}