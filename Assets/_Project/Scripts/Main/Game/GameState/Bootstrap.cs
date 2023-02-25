using _Project.Scripts.Extension;
using _Project.Scripts.Main.AppServices;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : IGameState
        {
            private SceneLoaderService _sceneLoaderService;

            [Inject]
            public void Construct(SceneLoaderService sceneLoaderService)
            {
                _sceneLoaderService = sceneLoaderService;
            }
            
            public async UniTask EnterState()
            {
                _sceneLoaderService.ShowScene();
                await 3f.WaitInSeconds();
            }
        }
    }
}