using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class CustomScene : GameState
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
                AppServices.Services.GameManagerService.PrepareToPlay();
            }
        }
    }
}