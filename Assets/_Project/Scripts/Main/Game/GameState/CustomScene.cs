using Cysharp.Threading.Tasks;
using Main.Contexts;
using Main.Services;

namespace Main.Game.GameState
{
    public static partial class GameStates
    {
        public class CustomScene : IGameState
        {
            private GameManagerService _gameManager;

            public async UniTask EnterState()
            {
                _gameManager ??= Context.GetService<GameManagerService>();
                await UniTask.Yield();
                _gameManager.PrepareToPlay();
            }
        }
    }
}