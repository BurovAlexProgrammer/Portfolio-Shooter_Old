using _Project.Scripts.Main.Settings;

namespace Main.Service
{
    public class SettingsServiceInstaller : BaseServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
    }
}