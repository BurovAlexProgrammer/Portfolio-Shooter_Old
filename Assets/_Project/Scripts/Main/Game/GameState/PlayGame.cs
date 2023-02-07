using _Project.Scripts.Extension;
using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class PlayGame : IGameState
        {
            public async UniTask EnterState()
            {
                Services.Services.AudioService.PlayMusic(AudioService.MusicPlayerState.Battle).Forget();
                Services.Services.ControlService.LockCursor();
                Services.Services.ControlService.Controls.Player.Enable();
                Services.Services.ControlService.Controls.Menu.Disable();
                await Services.Services.SceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MiniGameLevel);
                Services.Services.StatisticService.ResetSessionRecords();
            }

            public async UniTask ExitState()
            {
                Services.Services.AudioService.StopMusic();
                
                if (Time.timeScale == 0f)
                {
                    await DOVirtual.Float(0, 1f, 0.5f, x => Time.timeScale = x).AwaitForComplete();
                }

                Services.Services.ControlService.UnlockCursor();
                Services.Services.StatisticService.SaveToFile();
            }

            public UniTask Update()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}