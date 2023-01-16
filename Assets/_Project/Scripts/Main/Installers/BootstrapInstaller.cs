using System.IO;
using _Project.Scripts.Main.Localizations;
using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;
using static _Project.Scripts.Main.Services.Services;
using AudioService = _Project.Scripts.Main.Services.AudioService;

namespace _Project.Scripts.Main.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoaderServicePrefab;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private GameManagerService _gameManagerServicePrefab;
        [SerializeField] private LocalizationService _localizationServicePrefab;
        [SerializeField] private ControlService _controlServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private PoolService _poolServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;

        public override void InstallBindings()
        {
            Application.logMessageReceived += LogToFile;
            InstallSceneLoaderService();
            InstallScreenService();
            InstallAudioService();
            InstallGameManagerService();
            InstallSettingService();
            InstallLocalizationService();
            InstallControlService();
            InstallDebugService();
            InstallPoolService();
        }
        
        

        private void InstallAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(_audioServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((AudioService)instance))
                .NonLazy();
        }

        private void InstallPoolService()
        {
            Container
                .Bind<PoolService>()
                .FromComponentInNewPrefab(_poolServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((PoolService)instance))
                .NonLazy();
        }

        private void InstallDebugService()
        {
            Container
                .Bind<DebugService>()
                .FromComponentInNewPrefab(_debugServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((DebugService)instance))
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
                .FromComponentInNewPrefab(_controlServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                    (instance as ControlService)?.Init())
                .NonLazy();
        }

        private void InstallSettingService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(_settingsServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var service = (SettingsService)instance;
                    service.Init();
                    service.Load();
                }).NonLazy();
        }

        private void InstallScreenService()
        {
            Container
                .Bind<ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService((ScreenService)instance))
                .NonLazy();
        }

        private void InstallSceneLoaderService()
        {
           Container
                .Bind<SceneLoaderService>()
                .FromComponentInNewPrefab(_sceneLoaderServicePrefab)
                .AsSingle()
                .NonLazy();
        }

        private void InstallGameManagerService()
        {
            Container
                .Bind<GameManagerService>()
                .FromComponentInNewPrefab(_gameManagerServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => SetService(instance as GameManagerService))
                .NonLazy();
        }
        
        void InstallLocalizationService()
        {
            Container
                .Bind<LocalizationService>()
                .FromComponentInNewPrefab(_localizationServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => (instance as LocalizationService)?.Init())
                .NonLazy();
        }
    }
}
