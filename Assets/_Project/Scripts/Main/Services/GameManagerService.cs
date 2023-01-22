using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project.Scripts.Main.Services
{
    public class GameManagerService : BaseService
    {
        [SerializeField, ReadOnlyField] private GameStateMachine _gameStateMachine;
        [SerializeField, ReadOnlyField] private bool _isGamePause;

        [Inject] private ControlService _controlService;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private LocalizationService _localizationService;
        [Inject] private StatisticService _statisticService;

        public Action<bool> SwitchPause;

        private bool _transaction;

        public GameStates CurrentGameState => _gameStateMachine.ActiveState;
        public bool IsGamePause => _isGamePause;

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

            _controlService.Controls.Player.Pause.BindAction(BindActions.Started, PauseGame);
            _controlService.Controls.Menu.Pause.BindAction(BindActions.Started, ReturnGame);
            _gameStateMachine.Init();
        }

        public void QuitGame()
        {
            _gameStateMachine.SetState(GameStates.GameQuit);
        }

        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            if (_transaction) return;

            if (CurrentGameState != GameStates.PlayGame && CurrentGameState != GameStates.CustomSceneBoot) return;

            var fixedDeltaTime = Time.fixedDeltaTime;
            _transaction = true;
            _isGamePause = true;
            _controlService.Controls.Player.Disable();
            SwitchPause?.Invoke(_isGamePause);

            await DOVirtual.Float(1f, 0f, 1f, SetTimeSpeed)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            _controlService.Controls.Menu.Enable();
            Time.fixedDeltaTime = fixedDeltaTime;
            _transaction = false;
        }

        public async void ReturnGame()
        {
            if (_transaction) return;

            if (CurrentGameState != GameStates.PlayGame && CurrentGameState != GameStates.CustomSceneBoot) return;

            _transaction = true;
            _isGamePause = false;
            SwitchPause?.Invoke(_isGamePause);
            _controlService.Controls.Player.Enable();

            await DOVirtual.Float(0f, 1f, 1f, SetTimeSpeed)
                .SetUpdate(true)
                .AsyncWaitForCompletion();

            _controlService.Controls.Menu.Disable();
            _transaction = false;
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            _statisticService.CalculateSessionDuration();
            _statisticService.SaveToFile();
        }

        private void ReturnGame(InputAction.CallbackContext ctx)
        {
            ReturnGame();
        }

        private void SetTimeSpeed(float value)
        {
            Time.timeScale = value;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}