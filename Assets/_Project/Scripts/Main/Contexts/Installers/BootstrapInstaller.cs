using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using Main.DTOs;
using Main.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Contexts.Installers
{
    public class BootstrapInstaller : ProjectContextInstaller
    {
        [SerializeField] private string _startupGameScene;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private ControlService _controlServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;

        protected override void InstallBindings()
        {
            DOTween.SetTweensCapacity(1000, 50);
            Context.Bind<ScreenService>().FromNewPrefab(_screenServicePrefab);
            Context.Bind<DebugService>().FromNewPrefab(_debugServicePrefab);
            Context.Bind<AudioService>().FromNewPrefab(_audioServicePrefab);
            Context.Bind<SettingsService>().FromNewPrefab(_settingsServicePrefab);
            Context.Bind<ControlService>().FromNewPrefab(_controlServicePrefab);
            Context.Bind<FileService>().As<IFileService>().FromNew();
            Context.Bind<SceneLoaderService>().FromNew();
            Context.Bind<PoolService>().FromNew();
            Context.Bind<LocalizationService>().FromNew();
            Context.Bind<StatisticService>().FromNew();
            Context.Bind<GameManagerService>().FromNew();
            
            StartCoroutine(LateStartup());
        }

        private IEnumerator LateStartup()
        {
            yield return null;
            
            if (Context.Resolve<SceneLoaderService>().IsInitialScene(Scenes.Boot))
            {
                SceneManager.LoadScene(_startupGameScene);
            }
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
    }
}