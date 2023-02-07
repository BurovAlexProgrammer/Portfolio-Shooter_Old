using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : IGameState
        {
            public async UniTask EnterState()
            {
                SceneLoaderService.ShowScene();
                await 3f.WaitInSeconds();
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