using System;
using Main.Extension;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Main.Contexts;
using Main.Contexts.DI;
using Main.Game.GameState;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Main.Game.CharacterController;

namespace Main.Services
{
    public class GameManagerService : IService, IConstruct
    {
        private GameStateMachine _gameStateMachine;
        private bool _isGamePause;
        private int _scores;

        [Inject] private ControlService _controlService;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private StatisticService _statisticService;
        [Inject] private AudioService _audioService;

        public event Action<bool> SwitchPause;
        public event Action GameOver;

        private bool _transaction;
        private bool _isGameOver;

        public IGameState ActiveGameState => _gameStateMachine.ActiveState;
        public bool IsGamePause => _isGamePause;
        public bool IsGameOver => _isGameOver;
        public int Scores => _scores;

        public void Construct()
        {
            _gameStateMachine = new GameStateMachine();
            _controlService.Controls1.Player.Pause.BindAction(BindActions.Started, PauseGame);
        }

        public async UniTask SetGameState<T>() where T : IGameState
        {
            await _gameStateMachine.SetState<T>();
        }

        private void Start()
        {
            _gameStateMachine.Start().Forget();
        }

        public void RestartGame()
        {
            _isGameOver = false;
            RestoreTimeSpeed();
            _statisticService.EndGameDataSaving(this);
            _gameStateMachine.SetState<GameStates.RestartGame>().Forget();
            _gameStateMachine.SetState<GameStates.PlayNewGame>().Forget();
        }

        public void QuitGame()
        {
            _gameStateMachine.SetState<GameStates.QuitGame>().Forget();
        }

        public void GoToMainMenu()
        {
            _statisticService.EndGameDataSaving(this);
            _gameStateMachine.SetState<GameStates.MainMenu>().Forget();
        }

        public void PrepareToPlay()
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.Battle);
            _controlService.LockCursor();
            _controlService.Controls1.Player.Enable();
            _controlService.Controls1.Menu.Disable();
            _statisticService.ResetSessionRecords();
        }

        public async void PauseGame(InputAction.CallbackContext ctx)
        {
            if (_transaction) return;

            if (ActiveStateEquals<GameStates.PlayNewGame>() == false &&
                ActiveStateEquals<GameStates.CustomScene>() == false) return;

            Debug.Log("Game paused to menu.");

            var fixedDeltaTime = Time.fixedDeltaTime;
            _transaction = true;
            _isGamePause = true;
            _controlService.Controls1.Player.Disable();
            _controlService.UnlockCursor();
            SwitchPause?.Invoke(_isGamePause);

            await FluentSetTimeScale(0f);

            _controlService.Controls1.Menu.Enable();
            Time.fixedDeltaTime = fixedDeltaTime;
            _transaction = false;
        }

        public async void ReturnGame()
        {
            if (_isGameOver) return;
            if (_transaction) return;

            if (ActiveStateEquals<GameStates.PlayNewGame>() == false &&
                ActiveStateEquals<GameStates.CustomScene>() == false) return;

            Debug.Log("Game returned from pause.");
            _transaction = true;
            _isGamePause = false;
            SwitchPause?.Invoke(_isGamePause);
            _controlService.Controls1.Player.Enable();
            _controlService.LockCursor();

            await FluentSetTimeScale(1f);

            _controlService.Controls1.Menu.Disable();
            _transaction = false;
        }

        public async void RunGameOver()
        {
            Debug.Log("Game Over");
            _statisticService.EndGameDataSaving(this);
            _controlService.Controls1.Player.Disable();

            await FluentSetTimeScale(1f);

            _controlService.UnlockCursor();
            _controlService.Controls1.Menu.Enable();

            _isGameOver = true;
            GameOver?.Invoke();
        }

        public bool ActiveStateEquals<T>() where T : IGameState
        {
            return ActiveGameState.EqualsState(typeof(T));
        }

        public void RestoreTimeSpeed()
        {
            SetTimeScale(1f);
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

        private void AddScoresOnCharacterDead(CharacterController characterController)
        {
            AddScores(characterController.Data.Score);
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