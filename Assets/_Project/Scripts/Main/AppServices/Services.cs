using System.Runtime.CompilerServices;
using _Project.Scripts.Main.AppServices.SceneServices;

namespace _Project.Scripts.Main.AppServices
{
    public static class Services
    {
        public static ScreenService ScreenService { get; private set; }
        public static SceneLoaderService SceneLoaderService { get; private set; }
        public static GameManagerService GameManagerService { get; private set; }
        public static LocalizationService LocalizationService { get; private set; }
        public static DebugService DebugService { get; private set; }
        public static PoolService PoolService { get; private set; }
        public static AudioService AudioService { get; private set; }
        public static StatisticService StatisticService { get; private set; }
        public static EventListenerService EventListenerService { get; private set; }
        public static BrainControlService BrainControl { get; private set; }
        public static ControlService ControlService { get; private set; }

        public static void SetService<T>(T instance) where T : BaseService
        {
            switch (instance)
            {
                case ScreenService service:
                    ScreenService = service;
                    break;
                case SceneLoaderService service:
                    SceneLoaderService = service;
                    break;
                case GameManagerService service:
                    service.Init();
                    GameManagerService = service;
                    break;
                case LocalizationService service:
                    LocalizationService = service;
                    break;
                case DebugService service:
                    DebugService = service;
                    break;
                case PoolService service:
                    service.Init();
                    PoolService = service;
                    break;
                case AudioService service:
                    service.Init();
                    AudioService = service;
                    break;
                case StatisticService service:
                    service.Init();
                    StatisticService = service;
                    break;
                case EventListenerService service:
                    EventListenerService = service;
                    break;
                case BrainControlService service:
                    BrainControl = service;
                    break;
                case ControlService service:
                    service.Init();
                    ControlService = service;
                    break;
                default:
                    throw new SwitchExpressionException();
            }
        }
        
        public static void KillService<T>(T instance) where T : BaseService
        {
            switch (instance)
            {
                case AppServices.ScreenService:
                    ScreenService = null;
                    break;
                case AppServices.SceneLoaderService:
                    SceneLoaderService = null;
                    break;
                case AppServices.GameManagerService:
                    GameManagerService = null;
                    break;
                case AppServices.LocalizationService:
                    LocalizationService = null;
                    break;
                case AppServices.DebugService:
                    DebugService = null;
                    break;
                case AppServices.PoolService:
                    PoolService = null;
                    break;
                case AppServices.AudioService:
                    AudioService = null;
                    break;
                case AppServices.StatisticService:
                    StatisticService = null;
                    break;
                case AppServices.EventListenerService:
                    EventListenerService = null;
                    break;
            }
        }
    }
}