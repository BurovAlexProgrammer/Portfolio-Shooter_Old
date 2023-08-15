using System.Collections;
using System.IO;
using _Project.Scripts.Main.AppServices;
using DG.Tweening;
using Main.Contexts;
using Main.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Main.Contexts.Installers
{
    public class BootstrapInstaller : MonoBehaviour
    {
        [SerializeField] private string _startupGameScene;
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake()
        {
            gameObject.name = "Services";
            DOTween.SetTweensCapacity(1000, 50);
            Context.RegisterService<ControlService>();
            Context.RegisterService<FileService>(); //How to Register IFileService 
            Context.RegisterService<ScreenService>(_screenServiceInstaller);
            Context.RegisterService<PoolService>();
            Context.RegisterService<DebugService>(_debugServiceInstaller);
            Context.RegisterService<AudioService>(_audioServiceInstaller);
            Context.RegisterService<SettingsService>(_settingsServiceInstaller);
            Context.RegisterService<LocalizationService>();
            
            // if (_debugServicePrefab.SaveLogToFile)
            // {
            //     Application.logMessageReceived += LogToFile;
            // }
            
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
