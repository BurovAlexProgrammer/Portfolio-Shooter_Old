using Main.Extension;
using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;

namespace Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : IGameState
        {
            private SceneLoaderService _sceneLoaderService;

            public async UniTask EnterState()
            {
                _sceneLoaderService ??= Context.GetService<SceneLoaderService>();
                await 3f.WaitInSeconds();
            }
        }
    }
}