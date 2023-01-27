using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using _Project.Scripts.Main.Services;
using DG.Tweening;
using UnityEngine.SceneManagement;
using static _Project.Scripts.Extension.Common;
using SceneName = _Project.Scripts.Main.Services.SceneLoaderService.Scenes;

namespace _Project.Scripts.Main
{
    public class GameStateMachine : MonoBehaviour
    {
        public Action StateChanged;
        
        private GameStates _activeState = GameStates.None;

        [Inject] private AudioService _audioService;
        [Inject] private SceneLoaderService _sceneLoader;
        [Inject] private ControlService _controlService;
        [Inject] private StatisticService _statisticService;

        public GameStates ActiveState => _activeState;

        public void Init()
        {
            if (_sceneLoader.InitialSceneEquals(SceneName.Boot))
            {
                SetState(GameStates.Boot);
                return;
            }
            
            SetState(GameStates.CustomSceneBoot);
        }

        public async void SetState(GameStates newState)
        {
            await ExitState(_activeState);
            await EnterState(newState);
            StateChanged?.Invoke();
        }

        private async UniTask EnterState(GameStates newState)
        {
            if (_activeState == newState)
            {
                Debug.Log("GameState Enter: " + newState + " (Already entered, skipped)", this);
                return;
            }

            _activeState = newState;
            Debug.Log("GameState Enter: " + newState, this);

            switch (newState)
            {
                case GameStates.CustomSceneBoot:
                    EnterStateCustomBoot();
                    break;
                case GameStates.Boot:
                    await EnterStateBoot();
                    break;
                case GameStates.MainMenu:
                    EnterStateMainMenu();
                    break;
                case GameStates.PlayGame:
                    EnterStatePlayGame();
                    break;
                case GameStates.GameQuit:
                    EnterStateQuitGame();
                    break;
                case GameStates.RestartGame:
                    await EnterStateRestartGame();
                    break;
            }
        }

        private async UniTask ExitState(GameStates oldState)
        {
            Debug.Log("GameState Exit: " + oldState, this);
            switch (oldState)
            {
                case GameStates.Boot:
                    await ExitStateBoot();
                    break;
                case GameStates.PlayGame:
                    ExitStatePlayGame();
                    break;
                case GameStates.GamePause:
                    break;
                case GameStates.CustomSceneBoot:
                    ExitStateCustomBoot();
                    break;
            }
        }
        
        private async UniTask EnterStateRestartGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            var newScene = SceneManager.CreateScene("Empty");
            newScene.SetActive(true);
            
            await SceneManager.UnloadSceneAsync(currentScene);
            
            SetState(GameStates.PlayGame);
        }

        private void EnterStateQuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void ExitStateCustomBoot()
        {
            _audioService.StopMusic();
        }

        private async UniTask EnterStateBoot()
        {
            _sceneLoader.ShowScene();
            await Wait(3f);
            SetState(GameStates.MainMenu);
        }

        private void EnterStateCustomBoot()
        {
            _ = _audioService.PlayMusic(AudioService.MusicPlayerState.Battle);
            _sceneLoader.ShowScene();
        }

        private void EnterStatePlayGame()
        {
            _ = _audioService.PlayMusic(AudioService.MusicPlayerState.Battle);
            _controlService.LockCursor();
            _controlService.Controls.Player.Enable();
            _controlService.Controls.Menu.Disable();
            _sceneLoader.LoadSceneAsync(SceneName.MiniGameLevel);
            _statisticService.ResetSessionRecords();
        }
        
        private void ExitStatePlayGame()
        {
            _audioService.StopMusic();
            if (Time.timeScale == 0f)
            {
                DOVirtual.Float(0, 1f, 0.5f, x => Time.timeScale = x);
            } 
            _controlService.UnlockCursor();
            _statisticService.SaveToFile();
        }

        private async UniTask ExitStateBoot()
        {
            await UniTask.Delay(1);
        }

        private void EnterStateMainMenu()
        {
            _ = _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
            _sceneLoader.LoadSceneAsync(SceneName.MainMenu);
        }
    }

    public enum GameStates
    {
         None, Boot, MainMenu, PlayGame, GamePause, GameQuit, CustomSceneBoot,
         RestartGame
    }
}