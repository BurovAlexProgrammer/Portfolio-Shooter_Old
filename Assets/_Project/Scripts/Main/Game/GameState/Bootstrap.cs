using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Service;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : IGameState
        {
            private SceneLoaderService _sceneLoaderService;

            public async UniTask EnterState()
            {
                _sceneLoaderService ??= Context.GetService<SceneLoaderService>();
                _sceneLoaderService.ShowScene();
                await 3f.WaitInSeconds();
            }
        }
    }
}