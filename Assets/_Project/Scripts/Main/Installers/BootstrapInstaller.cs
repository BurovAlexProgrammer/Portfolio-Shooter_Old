using _Project.Scripts.Main.Services;
using UnityEngine;
using Zenject;
using static _Project.Scripts.Main.Services.Services;

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

        public override void InstallBindings()
        {
            InstallSceneLoaderService();
            InstallScreenService();
            InstallGameManagerService();
            InstallSettingService();
            InstallLocalizationService();
            InstallControlService();
        }

        private void InstallControlService()
        {
            Container
                .Bind<ControlService>()
                .FromComponentInNewPrefab(_controlServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) =>
                {
                    var settingService = instance as ControlService;
                    settingService.Init();
                })
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
                    var settingService = (SettingsService)instance;
                    settingService.Init();
                    settingService.Load();
                });
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
                .OnInstantiated((ctx, instance) =>
                {
                    var serviceInstance = instance as GameManagerService;
                    SetService(serviceInstance);
                    serviceInstance.Init();
                })
                .NonLazy();
        }
        
        void InstallLocalizationService()
        {
            Container
                .Bind<LocalizationService>()
                .FromComponentInNewPrefab(_localizationServicePrefab)
                .AsSingle()
                .OnInstantiated((ctx, instance) => (instance as LocalizationService).Init())
                .NonLazy();
        }
    }
}
