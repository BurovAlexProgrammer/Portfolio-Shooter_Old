using System.Runtime.CompilerServices;
using _Project.Scripts.Main.AppServices.SceneServices;
using _Project.Scripts.Main.AppServices.SceneServices.PoolService;

namespace _Project.Scripts.Main.AppServices.Base
{
    public static class Services
    {
        private static readonly ServiceLocator<IService> _serviceLocator = new();
        public static ScreenService ScreenService { get; private set; }
        public static SceneLoaderService SceneLoaderService { get; private set; }
        public static GameManagerService GameManagerService { get; private set; }
        public static LocalizationService LocalizationService { get; private set; }
        public static DebugService DebugService { get; private set; }
        public static AudioService AudioService { get; private set; }
        public static StatisticService StatisticService { get; private set; }
        public static EventListenerService EventListenerService { get; private set; }
        public static ControlService ControlService { get; private set; }
        public static SettingsService SettingsService { get; private set; }

        private static T Get<T>() where T : IService => _serviceLocator.Get<T>();

        public static void RegisterService<T>(this T instance) where T : IService
        {
            _serviceLocator.Register(instance);
            
            switch (instance)
            {
                case SettingsService service:
                    SettingsService = service;
                    break;
                case ScreenService service:
                    ScreenService = service;
                    break;
                case SceneLoaderService service:
                    SceneLoaderService = service;
                    break;
                case GameManagerService service:
                    GameManagerService = service;
                    break;
                case LocalizationService service:
                    LocalizationService = service;
                    break;
                case DebugService service:
                    DebugService = service;
                    break;
                case AudioService service:
                    AudioService = service;
                    break;
                case StatisticService service:
                    StatisticService = service;
                    break;
                case EventListenerService service:
                    EventListenerService = service;
                    break;
                case ControlService service:
                    ControlService = service;
                    break;
                default:
                    throw new SwitchExpressionException();
            }
        }
    }
}