using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class CustomScene : IGameState
        {
            [Inject] private GameManagerService _gameManager;

            public async UniTask EnterState()
            {
                await UniTask.Yield();
                _gameManager.PrepareToPlay();
            }
        }
    }
}