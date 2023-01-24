using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.Game;
using Cysharp.Threading.Tasks;
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
        [SerializeField, ReadOnlyField] private int _scores;

        [Inject] private ControlService _controlService;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private StatisticService _statisticService;

        public Action<bool> SwitchPause;

        private bool _transaction;

        public GameStates CurrentGameState => _gameStateMachine.ActiveState;
        public bool IsGamePause => _isGamePause;

        public void SetGameState(GameStates newState)
        {
            _gameStateMachine.SetState(newState);
        }

        private void Awake()
        {
            Services.EventListenerService.CharacterDead += AddScoresOnCharacterDead;
        }

        private void OnDestroy()
        {
            Services.EventListenerService.CharacterDead -= AddScoresOnCharacterDead;
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
        
        public void RestartGame()
        {
            RestoreTimeSpeed();
            GameOver();
            _gameStateMachine.SetState(GameStates.RestartGame);
        }
        
        public void QuitGame()
        {
            _gameStateMachine.SetState(GameStates.GameQuit);
        }

        public void ReturnMainMenu()
        {
            GameOver();
            _gameStateMachine.SetState(GameStates.MainMenu);
        }

        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            Debug.Log("Game paused to menu.");
            if (_transaction) return;

            if (CurrentGameState != GameStates.PlayGame && CurrentGameState != GameStates.CustomSceneBoot) return;

            var fixedDeltaTime = Time.fixedDeltaTime;
            _transaction = true;
            _isGamePause = true;
            _controlService.Controls.Player.Disable();
            _controlService.UnlockCursor();
            SwitchPause?.Invoke(_isGamePause);

            await FluentSetTimeScale(0f);

            _controlService.Controls.Menu.Enable();
            Time.fixedDeltaTime = fixedDeltaTime;
            _transaction = false;
        }

        public async void ReturnGame()
        {
            if (_transaction) return;

            if (CurrentGameState != GameStates.PlayGame && CurrentGameState != GameStates.CustomSceneBoot) return;

            Debug.Log("Game returned from pause.");
            _transaction = true;
            _isGamePause = false;
            SwitchPause?.Invoke(_isGamePause);
            _controlService.Controls.Player.Enable();
            _controlService.LockCursor();
            
            await FluentSetTimeScale(1f);
            
            _controlService.Controls.Menu.Disable();
            _transaction = false;
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            _statisticService.SetScores(_scores);
            _statisticService.CalculateSessionDuration();
            _statisticService.SaveToFile();
        }

        public void AddScores(int value)
        {
            if (value < 0)
            {
                throw new Exception("Adding scores cannot be below zero.");
            }
            
            _scores += value;
            _statisticService.SetScores(_scores);
        }

        public void RestoreTimeSpeed()
        {
            SetTimeScale(1f);
        }

        private void AddScoresOnCharacterDead(Character character)
        {
            AddScores(character.Data.Score);
        }
        
        private void ReturnGame(InputAction.CallbackContext ctx)
        {
            ReturnGame();
        }

        private async UniTask FluentSetTimeScale(float scale)
        {
            var timeScale = Time.timeScale;
            await DOVirtual.Float(timeScale, scale, 1f, SetTimeScale)
                .SetUpdate(true)
                .AsyncWaitForCompletion();
        }
        
        private void SetTimeScale(float value)
        {
            Time.timeScale = value;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}