using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.DTOs;
using Main.Services;

namespace Main.Game.GameState
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
                await _sceneLoaderService.LoadSceneAsync(DTO.Scenes.MainMenu);
            }
        }
    }
}