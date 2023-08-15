using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class MainMenu : IGameState
        {
            private AudioService _audioService;
            private SceneLoaderService _sceneLoaderService;

            public async UniTask EnterState()
            {
                _audioService = Context.GetService<AudioService>();
                    _sceneLoaderService = Context.GetService<SceneLoaderService>();
                _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
                await _sceneLoaderService.LoadSceneAsync(SceneLoaderService.Scenes.MainMenu);
            }
        }
    }
}