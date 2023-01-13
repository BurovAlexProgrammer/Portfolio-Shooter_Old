using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : BaseService
    {
        [SerializeField] private GameStateMachine _gameStateMachine;
        [SerializeField] private bool _gamePause;

        [Inject] private ControlService _controlService;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private LocalizationService _localizationService;

        public Action<bool> SwitchPause;

        public GameStates CurrentGameState => _gameStateMachine.ActiveState;
        public bool GamePause => _gamePause;

        public void SetGameState(GameStates newState)
        {
            _gameStateMachine.SetState(newState);
        }

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

        public async void PauseGame()
        {
            _gamePause = true;
            SwitchPause?.Invoke(_gamePause);

            await DOVirtual.Float(1f, 0.3f, 0.3f, (x) => Time.timeScale = x)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            Time.timeScale = 0f;

            _controlService.Controls.Player.Disable();
            _controlService.Controls.Menu.Enable();
        }

        public async void ReturnGame()
        {
            _gamePause = false;
            SwitchPause?.Invoke(_gamePause);

            await DOVirtual.Float(0f, 1f, 0.3f, (x) => Time.timeScale = x)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            _controlService.Controls.Player.Enable();
            _controlService.Controls.Menu.Disable();
        }
    }
}