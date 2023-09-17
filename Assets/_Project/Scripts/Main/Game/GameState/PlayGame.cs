using Cysharp.Threading.Tasks;
using DG.Tweening;
using Main.Contexts;
using Main.DTOs;
using Main.Services;
using UnityEngine;

namespace Main.Game.GameState
{
    public static partial class GameStates
    {
        public class PlayNewGame : IGameState
        {
            private GameManagerService _gameManager;
            private AudioService _audioService;
            private ControlService _controlService;
            private SceneLoaderService _sceneLoaderService;
            private StatisticService _statisticService;

            public async UniTask EnterState()
            {
                _gameManager ??= Context.Resolve<GameManagerService>();
                _audioService ??= Context.Resolve<AudioService>();
                _controlService ??= Context.Resolve<ControlService>();
                _sceneLoaderService ??= Context.Resolve<SceneLoaderService>();
                _statisticService ??= Context.Resolve<StatisticService>();
                await UniTask.Yield();
                _gameManager.PrepareToPlay();
                await _sceneLoaderService.LoadSceneAsync(Scenes.MiniGameLevel);
            }

            public async UniTask ExitState()
            {
                _audioService.StopMusic();

                if (Time.timeScale == 0f)
                {
                    await DOVirtual.Float(0, 1f, 0.5f, x => Time.timeScale = x).AwaitForComplete();
                }

                _controlService.UnlockCursor();
                _statisticService.SaveToFile();
            }
        }
    }
}