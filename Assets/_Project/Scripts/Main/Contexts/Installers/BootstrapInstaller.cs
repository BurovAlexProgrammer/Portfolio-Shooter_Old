using System.IO;
using _Project.Scripts.Main.AppServices;
using _Project.Scripts.Main.AppServices.Base;
using DG.Tweening;
using UnityEngine;
using Zenject;
using AudioService = _Project.Scripts.Main.AppServices.AudioService;

namespace _Project.Scripts.Main.Contexts.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoaderServicePrefab;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private GameManagerService _gameManagerServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;

        public override void InstallBindings()
        {
            Services.Clear();
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
            InstallFileService();
            
            if (_debugServicePrefab.SaveLogToFile)
            {
                Application.logMessageReceived += LogToFile;
            }
        }

        private void InstallFileService()
        {
            Container
                .Bind<IFileService>()
                .To<FileService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallEventListenerService()
        {
            Container
                .Bind<EventListenerService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallStatisticService()
        {
            Container
                .Bind<StatisticService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void InstallAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(_audioServicePrefab)
                .WithGameObjectName("Audio Service")
                .AsSingle()
                .NonLazy();
        }

        private void InstallDebugService()
        {
            Container
                .Bind<DebugService>()
                .FromComponentInNewPrefab(_debugServicePrefab)
                .WithGameObjectName("Debug Service")
                .AsSingle()
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
                .NonLazy();
        }

        private void InstallScreenService()
        {
            Container
                .Bind<ScreenService>()
                .FromComponentInNewPrefab(_screenServicePrefab)
                .WithGameObjectName("Screen Service")
                .AsSingle()
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
                .NonLazy();
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
