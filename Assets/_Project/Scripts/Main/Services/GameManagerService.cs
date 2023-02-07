using System;
using _Project.Scripts.Extension;
using _Project.Scripts.Extension.Attributes;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Game.GameState;
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
        public Action GameOver;

        private bool _transaction;

        public IGameState CurrentGameState => _gameStateMachine.ActiveState;
        public bool IsGamePause => _isGamePause;
        public int Scores => _scores;

        public async UniTask SetGameState(IGameState newState)
        {
            await _gameStateMachine.SetState(newState);
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
            _gameStateMachine.Init().Forget();
        }

        public void RestartGame()
        {
            RestoreTimeSpeed();
            _statisticService.EndGameDataSaving(this);
            _gameStateMachine.SetState(new GameStates.RestartGame()).Forget();
            _gameStateMachine.SetState(new GameStates.PlayGame()).Forget();
        }

        public void QuitGame()
        {
            _gameStateMachine.SetState(new GameStates.QuitGame()).Forget();
        }

        public void GoToMainMenu()
        {
            _statisticService.EndGameDataSaving(this);
            _gameStateMachine.SetState(new GameStates.MainMenu()).Forget();
        }

        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            if (_transaction) return;

            if (IsCurrentState(typeof(GameStates.PlayGame)) == false &&
                IsCurrentState(typeof(GameStates.CustomScene)) == false) return;

            Debug.Log("Game paused to menu.");
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

            if (!IsCurrentState(typeof(GameStates.PlayGame)) && !IsCurrentState(typeof(GameStates.CustomScene))) return;

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

        public async void RunGameOver()
        {
            Debug.Log("Game Over");
            _statisticService.EndGameDataSaving(this);
            _controlService.Controls.Player.Disable();

            await FluentSetTimeScale(1f);

            _controlService.UnlockCursor();
            _controlService.Controls.Menu.Enable();
            GameOver?.Invoke();
        }

        public void RestoreTimeSpeed()
        {
            SetTimeScale(1f);
        }

        private bool IsCurrentState(Type type)
        {
            return CurrentGameState switch
            {
                null when type == null => true,
                null => false,
                _ => CurrentGameState.EqualsState(type)
            };
        }

        private void AddScores(int value)
        {
            if (value < 0)
            {
                throw new Exception("Adding scores cannot be below zero.");
            }

            _scores += value;
            _statisticService.SetScores(_scores);
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