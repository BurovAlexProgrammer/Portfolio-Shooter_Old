using _Project.Scripts.Main.Services;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class MainMenu : GameState
        {
            public override async UniTask EnterState()
            {
                Services.Services.AudioService.PlayMusic(AudioService.MusicPlayerState.MainMenu).Forget();
                await Services.Services.SceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}