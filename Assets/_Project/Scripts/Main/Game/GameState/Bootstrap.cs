using _Project.Scripts.Extension;
using Cysharp.Threading.Tasks;
using static _Project.Scripts.Main.AppServices.Services;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class Bootstrap : GameState
        {
            public override async UniTask EnterState()
            {
                SceneLoaderService.ShowScene();
                await 3f.WaitInSeconds();
            }
        }
    }
}