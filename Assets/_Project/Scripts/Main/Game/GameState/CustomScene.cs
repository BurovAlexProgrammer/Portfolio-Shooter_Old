using Cysharp.Threading.Tasks;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class CustomScene : IGameState
        {
            public async UniTask EnterState()
            {
                AudioService.StopMusic();
            }

            public async UniTask ExitState()
            {
            }

            public async UniTask Update()
            {
            }
        }
    }
}