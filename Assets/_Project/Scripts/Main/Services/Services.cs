using System.Runtime.CompilerServices;
using _Project.Scripts.Main.Services.SceneServices;

namespace _Project.Scripts.Main.Services
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
                case Main.Services.ScreenService:
                    ScreenService = null;
                    break;
                case Main.Services.SceneLoaderService:
                    SceneLoaderService = null;
                    break;
                case Main.Services.GameManagerService:
                    GameManagerService = null;
                    break;
                case Main.Services.LocalizationService:
                    LocalizationService = null;
                    break;
                case Main.Services.DebugService:
                    DebugService = null;
                    break;
                case Main.Services.PoolService:
                    PoolService = null;
                    break;
                case Main.Services.AudioService:
                    AudioService = null;
                    break;
                case Main.Services.StatisticService:
                    StatisticService = null;
                    break;
                case Main.Services.EventListenerService:
                    EventListenerService = null;
                    break;
            }
        }
    }
}