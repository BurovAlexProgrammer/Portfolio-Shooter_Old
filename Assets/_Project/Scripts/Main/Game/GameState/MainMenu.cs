using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class MainMenu : GameState
        {
            public override async UniTask EnterState()
            {
                Services.AudioService.PlayMusic(AudioService.MusicPlayerState.MainMenu).Forget();
                await Services.SceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}