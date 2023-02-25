using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class MainMenu : IGameState
        {
            [Inject] private AudioService _audioService;
            [Inject] private SceneLoaderService _sceneLoaderService;

            public async UniTask EnterState()
            {
                _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu).Forget();
                await _sceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}