using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class PlayNewGame : GameState
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
                Services.Services.GameManagerService.PrepareToPlay();
                await Services.Services.SceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MiniGameLevel);
            }

            public override async UniTask ExitState()
            {
                Services.Services.AudioService.StopMusic();
                
                if (Time.timeScale == 0f)
                {
                    await DOVirtual.Float(0, 1f, 0.5f, x => Time.timeScale = x).AwaitForComplete();
                }

                Services.Services.ControlService.UnlockCursor();
                Services.Services.StatisticService.SaveToFile();
            }
        }
    }
}