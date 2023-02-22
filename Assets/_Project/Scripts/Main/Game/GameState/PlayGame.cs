using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class PlayNewGame : IGameState
        {
            [Inject] private GameManagerService _gameManager;
            [Inject] private AudioService _audioService;
            [Inject] private ControlService _controlService;
            [Inject] private SceneLoaderService _sceneLoaderService;
            [Inject] private StatisticService _statisticService;

            public async UniTask EnterState()
            {
                await UniTask.Yield();
                _gameManager.PrepareToPlay();
                await _sceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MiniGameLevel);
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