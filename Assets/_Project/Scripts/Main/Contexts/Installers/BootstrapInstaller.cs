using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using Main.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.Contexts.Installers
{
    public class BootstrapInstaller : MonoBehaviour
    {
        [SerializeField] private string _startupGameScene;
        [SerializeField] private ScreenService _screenServicePrefab;
        [SerializeField] private ControlService _controlServicePrefab;
        [SerializeField] private DebugService _debugServicePrefab;
        [SerializeField] private SettingsService _settingsServicePrefab;
        [SerializeField] private AudioService _audioServicePrefab;

        public void Awake()
        {
            gameObject.name = "Services";
            DOTween.SetTweensCapacity(1000, 50);
            Context.BindService<ScreenService>(_screenServicePrefab);
            Context.BindService<DebugService>(_debugServicePrefab);
            Context.BindService<AudioService>(_audioServicePrefab); 
            Context.BindService<SettingsService>(_settingsServicePrefab);
            Context.BindService<ControlService>(_controlServicePrefab);
            Context.BindService<FileService>().As<IFileService>(); 
            Context.BindService<SceneLoaderService>();
            Context.BindService<PoolService>();
            Context.BindService<LocalizationService>();
            Context.BindService<StatisticService>();
            Context.BindService<GameManagerService>();

            Context.InitServices();

            StartCoroutine(LateStartup());
        }
        
        private IEnumerator LateStartup()
        {
            yield return null;
            SceneManager.LoadScene(_startupGameScene);
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
