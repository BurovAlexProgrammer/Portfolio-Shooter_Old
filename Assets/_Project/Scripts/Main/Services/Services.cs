using UnityEngine;

namespace _Project.Scripts.Main.Services
{
    public static class Services
    {
        public static ScreenService ScreenService { get; private set; }
        public static SceneLoaderService SceneLoaderService { get; private set; }
        public static GameManagerService GameManagerService { get; private set; }
        public static LocalizationService LocalizationService { get; private set; }

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
                    GameManagerService = service;
                    break;
                case LocalizationService service:
                    LocalizationService = service;
                    break;
            }
        }
    }

    public abstract class BaseService : MonoBehaviour
    {
        
    }
}