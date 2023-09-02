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
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake()
        {
            gameObject.name = "Services";
            DOTween.SetTweensCapacity(1000, 50);
            Context.BindService<ControlService>();
            // Context.BindService<FileService>(); //How to Register IFileService 
            Context.BindService<ScreenService>(_screenServiceInstaller);
            // Context.BindService<SceneLoaderService>();
            // Context.BindService<PoolService>();
            // Context.BindService<DebugService>(_debugServiceInstaller);
            // Context.BindService<AudioService>(_audioServiceInstaller);
            // Context.BindService<SettingsService>(_settingsServiceInstaller);
            // Context.BindService<LocalizationService>();
            // Context.BindService<StatisticService>();
            // Context.BindService<GameManagerService>();

            Context.InitServices();

            UnityEditor.EditorApplication.isPlaying = false;
            
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
