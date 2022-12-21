using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : BaseService
    {
        [SerializeField] private GameStateMachine _gameStateMachine;

        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private LocalizationService _localizationService;
        
        public GameStateMachine GameStateMachine => _gameStateMachine;

        public void Init()
        {
            if (string.IsNullOrEmpty(_sceneLoader.InitialScene.name))
            {
                _sceneLoader.Init();
            }
            _gameStateMachine.Init();
        }
        
        public void QuitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else 
            Application.Quit();
            #endif
        }
    }
}