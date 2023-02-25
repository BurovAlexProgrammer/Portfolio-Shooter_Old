using System.Runtime.CompilerServices;

namespace _Project.Scripts.Main.AppServices.Base
{
    public static class Services
    {
        private static readonly ServiceLocator<IService> _serviceLocator = new();
        public static ScreenService ScreenService { get; private set; }
        public static SceneLoaderService SceneLoader { get; private set; }
        public static GameManagerService GameManager { get; private set; }
        public static LocalizationService Localization { get; private set; }
        public static DebugService DebugService { get; private set; }
        public static AudioService AudioService { get; private set; }
        public static StatisticService Statistics { get; private set; }
        public static EventListenerService EventListener { get; private set; }
        public static ControlService ControlService { get; private set; }
        public static SettingsService Settings { get; private set; }
        public static FileService FileService { get; private set; }

        private static T Get<T>() where T : IService => _serviceLocator.Get<T>();

        public static void Clear()
        {
            _serviceLocator.Clear();
            ScreenService = null;
            SceneLoader = null;
            GameManager = null;
            Localization = null;
            DebugService = null;
            AudioService = null;
            Statistics = null;
            EventListener = null;
            ControlService = null;
            Settings = null;
            FileService = null;
        }

        public static void RegisterService<T>(this T instance) where T : IService
        {
            _serviceLocator.Register(instance);

            switch (instance)
            {
                case SettingsService service:
                    Settings = service;
                    break;
                case ScreenService service:
                    ScreenService = service;
                    break;
                case SceneLoaderService service:
                    SceneLoader = service;
                    break;
                case GameManagerService service:
                    GameManager = service;
                    break;
                case LocalizationService service:
                    Localization = service;
                    break;
                case DebugService service:
                    DebugService = service;
                    break;
                case AudioService service:
                    AudioService = service;
                    break;
                case StatisticService service:
                    Statistics = service;
                    break;
                case EventListenerService service:
                    EventListener = service;
                    break;
                case ControlService service:
                    ControlService = service;
                    break;
                case FileService service:
                    FileService = service;
                    break;
                default:
                    throw new SwitchExpressionException();
            }
        }
    }
}