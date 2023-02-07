using Cysharp.Threading.Tasks;
using static _Project.Scripts.Main.Services.Services;

namespace _Project.Scripts.Main.Game.GameState
{
    public static partial class GameStates
    {
        public class CustomScene : GameState
        {
            public override async UniTask EnterState()
            {
                await UniTask.Yield();
                AudioService.StopMusic();
            }
        }
    }
}