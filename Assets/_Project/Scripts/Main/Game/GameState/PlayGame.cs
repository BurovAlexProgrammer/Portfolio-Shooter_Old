using _Project.Scripts.Main.AppServices;
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
                Services.GameManagerService.PrepareToPlay();
                await Services.SceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MiniGameLevel);
            }

            public override async UniTask ExitState()
            {
                Services.AudioService.StopMusic();
                
                if (Time.timeScale == 0f)
                {
                    await DOVirtual.Float(0, 1f, 0.5f, x => Time.timeScale = x).AwaitForComplete();
                }

                Services.ControlService.UnlockCursor();
                Services.StatisticService.SaveToFile();
            }
        }
    }
}