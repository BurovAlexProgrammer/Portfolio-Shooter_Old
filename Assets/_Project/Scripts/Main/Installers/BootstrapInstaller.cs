using System.IO;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.Game.GameState;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using AudioService = _Project.Scripts.Main.AppServices.AudioService;

namespace _Project.Scripts.Main.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoaderServicePrefab;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private GameManagerService _gameManagerServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;
        [SerializeField] private StatisticService _statisticServicePrefab;
        [SerializeField] private EventListenerService _eventListenerServicePrefab;

        public override void InstallBindings()
        {
            gameObject.name = "Services";
            DOTween.SetTweensCapacity(1000, 50);
            InstallEventListenerService();
            InstallSceneLoaderService();
            InstallScreenService();
            InstallAudioService();
            InstallGameManagerService();
            InstallSettingService();
            InstallLocalizationService();
            InstallControlService();
            InstallDebugService();
            InstallStatisticService();
            
            if (_debugServicePrefab.SaveLogToFile)
            {
                Application.logMessageReceived += LogToFile;
            }
        }

        private void InstallEventListenerService()
        {
            Container
                .Bind<EventListenerService>()
                .FromComponentInNewPrefab(_eventListenerServicePrefab)
                .WithGameObjectName("Event Listener")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService(instance as EventListenerService))
                .NonLazy();
        }

        private void InstallStatisticService()
        {
            Container
                .Bind<StatisticService>()
                .FromComponentInNewPrefab(_statisticServicePrefab)
                .WithGameObjectName("Statistic Service")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService((StatisticService)instance))
                .NonLazy();
        }

        private void InstallAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(_audioServicePrefab)
                .WithGameObjectName("Audio Service")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService((AudioService)instance))
                .NonLazy();
        }

        private void InstallDebugService()
        {
            Container
                .Bind<DebugService>()
                .FromComponentInNewPrefab(_debugServicePrefab)
                .WithGameObjectName("Debug Service")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService((DebugService)instance))
                .NonLazy();
        }

        private void LogToFile(string condition, string stacktrace, LogType type)
        {
            var path = Application.persistentDataPath + "/log.txt";
            using var streamWriter = File.AppendText(path);
            streamWriter.WriteLine($"{condition}");
            streamWriter.WriteLine("----");
            streamWriter.WriteLine($"{stacktrace}");
            streamWriter.WriteLine("-----------------------------------------------------------------------------------------");
        }

        private void InstallControlService()
        {
            Container
                .Bind<ControlService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallSettingService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(_settingsServicePrefab)
                .WithGameObjectName("Settings Service")
                .AsSingle()
                // .OnInstantiated((ctx, instance) =>
                // {
                //     (instance as MonoServiceBase).Init();
                //     // service.Init();
                //     // service.Load();
                // })
                .NonLazy();
        }

        private void InstallScreenService()
        {
            Container
                .Bind<ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .WithGameObjectName("Screen Service")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService((ScreenService)instance))
                .NonLazy();
        }

        private void InstallSceneLoaderService()
        {
           Container
                .Bind<SceneLoaderService>()
                .FromComponentInNewPrefab(_sceneLoaderServicePrefab)
                .WithGameObjectName("Scene Loader")
                .AsSingle()
                .NonLazy();
        }

        private void InstallGameManagerService()
        {
            Container
                .Bind<GameManagerService>()
                .FromComponentInNewPrefab(_gameManagerServicePrefab)
                .WithGameObjectName("Game Manager")
                .AsSingle()
                // .OnInstantiated((ctx, instance) => SetService(instance as GameManagerService))
                .NonLazy();
            //Container.Resolve<GameStateMachine>();
            
        }
        
        void InstallLocalizationService()
        {
            Container
                .Bind<LocalizationService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
